using eshop.Application.Categories;
using eshop.Application.Orders;
using Microsoft.AspNetCore.Mvc;

namespace eshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly CreateOrderService _createOrderService;

        public OrderController(CreateOrderService createOrderService)
        {
            _createOrderService = createOrderService;
        }
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create([FromBody] int userId)
        {
            var result = await _createOrderService.CreateOrderAsync(userId, CancellationToken.None);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(Create), new { result.Value.Id }, result.Value);
            }

            return BadRequest(result.Errors);

        }
    }
}
