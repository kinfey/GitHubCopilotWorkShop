using Microsoft.AspNetCore.Mvc;
using ChengFenStore.Services;
using ChengFenStore.Models;

namespace ChengFenStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            var createdOrder = _orderService.CreateOrder(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPut("confirm/{id}")]
        public IActionResult ConfirmOrder(int id)
        {
            var order = _orderService.ConfirmOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("track/{id}")]
        public IActionResult TrackOrder(int id)
        {
            var order = _orderService.TrackOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
