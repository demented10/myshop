using eshop.Application.BasketItems;
using eshop.Application.Baskets;
using eshop.Application.Categories;
using eshop.Application.OrderItems;
using eshop.Application.Orders;
using Microsoft.AspNetCore.Mvc;

namespace eshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly CreateOrderService _createOrderService;
        private readonly GetOrderService _getOrderService;

        public OrderController(CreateOrderService createOrderService, GetOrderService getOrderService)
        {
            _createOrderService = createOrderService;
            _getOrderService = getOrderService;
        }
        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create([FromBody] int userId)
        {
            var result = await _createOrderService.CreateOrderAsync(userId, CancellationToken.None);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(Create), new { result.Value.Id }, result.Value);
            }

            return BadRequest(result.Errors);

        }

        [HttpGet("getOrder/{orderId:int}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int orderId)
        {
            var result = await _getOrderService.GetOrderByIdAsync(orderId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }
        [HttpGet("getOrders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var result = await _getOrderService.GetAllOrdersAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }
        [HttpGet("{orderId:int}/getOrderItems")]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetOrderItems(int orderId)
        {
            var result = await _getOrderService.GetAllOrderItemsAsync(orderId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }
        [HttpGet("getUserOrders/{userId:int}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetUserOrders(int userId)
        {
            var result = await _getOrderService.GetAllUserOrdersAsync(userId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }
    }
}
