using PaymentService.Database.Entities;
using PaymentService.Models;
using Razorpay.Api;

namespace PaymentService.Services.Interfaces
{
    public interface IPaymentRepository
    {
        string CreateOrder(RazorPayOrder order);
        Payment GetPaymentDetails(string paymentId);
        bool SavePaymentDetails(PaymentDetail model);
        string VerifyPayment(PaymentConfirmModel payment);
    }
}
