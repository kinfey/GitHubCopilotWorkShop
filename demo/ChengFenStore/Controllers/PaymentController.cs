using Microsoft.AspNetCore.Mvc;
using ChengFenStore.Services;
using ChengFenStore.Models;

namespace ChengFenStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("process")]
        public IActionResult ProcessPayment([FromBody] Payment payment)
        {
            var result = _paymentService.ProcessPayment(payment);
            if (result)
            {
                return Ok(new { message = "Payment processed successfully" });
            }
            return BadRequest(new { message = "Payment processing failed" });
        }

        [HttpGet("records")]
        public IActionResult GetPaymentRecords()
        {
            var records = _paymentService.GetPaymentRecords();
            return Ok(records);
        }
    }
}
