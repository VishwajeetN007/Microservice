using WebApp.Helpers;
using WebApp.HttpClients;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc;
//using WebApp.Helpers;
//using WebApp.Messages;

namespace WebApp.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly PaymentService _paymentService;
       // private readonly IServiceBusHelper _serviceBusHelper;
        CartService _cartService;
        IConfiguration _configuration;
        public PaymentController(CartService cartService, IConfiguration configuration, PaymentService paymentService 
            //,IServiceBusHelper serviceBusHelper
            )
        {
            _cartService = cartService;
            _paymentService = paymentService;
            _configuration = configuration;
            //_serviceBusHelper = serviceBusHelper;
        }

        public async Task<IActionResult> Index()
        {
            PaymentModel payment = new PaymentModel();
            CartModel cart = await _cartService.GetUserCartAsync(CurrentUser.Id);
            if (cart != null)
            {
                payment.Cart = cart;
                payment.GrandTotal = Math.Round(cart.GrandTotal);
                payment.Currency = "INR";
                payment.Description = string.Join(",", cart.CartItems.Select(r => r.Name));
                payment.RazorpayKey = _configuration["Razorpay:Key"];
                payment.Receipt = Guid.NewGuid().ToString(); //Merchant Transaction Id

                RazorPayOrder razorPayOrder = new RazorPayOrder
                {
                    Amount = Convert.ToInt32(payment.GrandTotal * 100),
                    Currency = payment.Currency,
                    Receipt = payment.Receipt
                };
                payment.OrderId = await _paymentService.CreateOrderAsync(razorPayOrder);

                return View(payment);
            }
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public async Task<IActionResult> Status(IFormCollection form)
        {
            try
            {
                if (form.Keys.Count > 0 && !string.IsNullOrWhiteSpace(form["rzp_paymentid"]))
                {
                    string paymentId = form["rzp_paymentid"];
                    string orderId = form["rzp_orderid"];
                    string signature = form["rzp_signature"];
                    string transactionId = form["Receipt"];
                    string currency = form["Currency"];

                    PaymentConfirmModel payment = new PaymentConfirmModel
                    {
                        PaymentId = paymentId,
                        OrderId = orderId,
                        Signature = signature
                    };
                    string Status = await _paymentService.VerifyPaymentAsync(payment);

                    if (Status == "captured" || Status == "completed")
                    {
                        CartModel cart = await _cartService.GetUserCartAsync(CurrentUser.Id);
                        TransactionModel model = new TransactionModel();

                        model.CartId = cart.Id;
                        model.Total = cart.Total;
                        model.Tax = cart.Tax;
                        model.GrandTotal = cart.GrandTotal;
                        model.CreatedDate = DateTime.Now;

                        model.Status = Status;
                        model.TransactionId = transactionId;
                        model.Currency = currency;
                        model.Email = CurrentUser.Email;
                        model.Id = paymentId;
                        model.UserId = CurrentUser.Id;

                        bool status = await _paymentService.SavePaymentDetailsAsync(model);
                        if (status)
                        {
                            //AddressModel address = TempData.Get<AddressModel>("Address");

                            //Send Order Initiate Command to Service Bus
                            //OrderMessage orderMessage = new OrderMessage
                            //{
                            //    PaymentId = payment.PaymentId,
                            //    CartId = cart.Id,
                            //    UserId = CurrentUser.Id,
                            //    Products = string.Join(",", cart.CartItems.Select(p => $"{p.ItemId}:{p.Quantity}"))
                            //};

                            //await _serviceBusHelper.SendPaymentMessage(orderMessage);

                            //make cart inactive
                            await _cartService.MakeCartInActiveAsync(cart.Id);

                            //TO DO: Send email
                            TempData.Set("PaymentDetails", model);
                            return RedirectToAction("Receipt");
                        }
                        else
                        {
                            ViewBag.Message = "Due to some technical issues we are not able to receive order details. We will contact you soon..";
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            ViewBag.Message = "Your payment has been failed. You can contact us at support@dotnettricks.com.";
            return View();
        }

        public IActionResult Receipt()
        {
            TransactionModel model = TempData.Peek<TransactionModel>("PaymentDetails");
            return View(model);
        }
    }
}
