using PaymentService.Database;
using PaymentService.Database.Entities;
using PaymentService.Models;
using PaymentService.Services.Interfaces;
using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;

namespace PaymentService.Services.Implementations
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly RazorpayClient _client;
        IConfiguration _configuration;
        AppDbContext _db;
        public PaymentRepository(IConfiguration configuration, AppDbContext db)
        {
            _db = db;
            _configuration = configuration;
            if (_client == null)
            {
                _client = new RazorpayClient(_configuration["RazorPay:Key"], _configuration["RazorPay:Secret"]);
            }
        }
        private static string getActualSignature(string payload, string secret)
        {
            byte[] secretBytes = StringEncode(secret);
            HMACSHA256 hashHmac = new HMACSHA256(secretBytes);
            var bytes = StringEncode(payload);

            return HashEncode(hashHmac.ComputeHash(bytes));
        }

        private static byte[] StringEncode(string text)
        {
            var encoding = new ASCIIEncoding();
            return encoding.GetBytes(text);
        }

        private static string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        public string CreateOrder(RazorPayOrder order)
        {
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", order.Amount); // amount in the smallest currency unit
            options.Add("receipt", order.Receipt);
            options.Add("currency", order.Currency);
            Razorpay.Api.Order data = _client.Order.Create(options);
            return data["id"].ToString();
        }

        public Payment GetPaymentDetails(string paymentId)
        {
            return _client.Payment.Fetch(paymentId);
        }

        public bool SavePaymentDetails(PaymentDetail model)
        {
            try
            {
                _db.PaymentDetails.Add(model);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string VerifyPayment(PaymentConfirmModel payment)
        {
            string payload = string.Format("{0}|{1}", payment.OrderId, payment.PaymentId);
            string secret = RazorpayClient.Secret;
            string actualSignature = getActualSignature(payload, secret);
            bool status =  actualSignature.Equals(payment.Signature);
            if (status)
            {
                Payment paymentDetails = GetPaymentDetails(payment.PaymentId);
                return paymentDetails["status"].ToString();
            }
            return "";
        }
    }
}
