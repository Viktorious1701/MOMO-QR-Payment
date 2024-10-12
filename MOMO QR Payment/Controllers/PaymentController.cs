using Microsoft.AspNetCore.Mvc;
using MOMO_QR_Payment.Models;
using MOMO_QR_Payment.Services;

namespace MOMO_QR_Payment.Controllers
{
    // Controllers/PaymentController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMomoService _momoService;

        public PaymentController(IMomoService momoService)
        {
            _momoService = momoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CourseInfoModel courseInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderInfo = new OrderInfoModel
            {
                OrderInfo = courseInfo.CourseInfo,
                Amount = 100000 // Set your amount here
            };

            var result = await _momoService.CreatePaymentAsync(orderInfo);
            return Ok(result);
        }

        [HttpGet("callback")]
        public IActionResult PaymentCallback([FromQuery] string orderId, [FromQuery] string resultCode)
        {
            // Handle the payment callback here
            // You should verify the payment status with Momo's API
            if (resultCode == "0")
            {
                return Ok(new { Message = "Payment successful", OrderId = orderId });
            }
            else
            {
                return BadRequest(new { Message = "Payment failed", OrderId = orderId });
            }
        }
    }
}
