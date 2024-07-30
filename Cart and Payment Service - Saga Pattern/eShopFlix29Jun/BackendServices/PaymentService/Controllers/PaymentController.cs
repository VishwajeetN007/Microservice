using Microsoft.AspNetCore.Mvc;
using PaymentService.Database.Entities;
using PaymentService.Models;
using PaymentService.Services.Interfaces;

namespace PaymentService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        IPaymentRepository _paymentRepository;
        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpPost]
        public IActionResult CreateOrder(RazorPayOrder order)
        {
            try
            {
                string orderId = _paymentRepository.CreateOrder(order);
                return Ok(orderId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult VerifyPayment(PaymentConfirmModel payment)
        {
            try
            {
                string status = _paymentRepository.VerifyPayment(payment);
                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult SavePaymentDetails(PaymentDetail payment)
        {
            try
            {
                bool status = _paymentRepository.SavePaymentDetails(payment);
                if (status)
                    return CreatedAtAction("SavePaymentDetails", status);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
