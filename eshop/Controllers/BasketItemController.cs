using eshop.Application.BasketItems;
using Microsoft.AspNetCore.Mvc;

namespace eshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketItemController : Controller
    {
        private readonly CreateBasketItemService _createBasketItemService;

        public BasketItemController(CreateBasketItemService createBasketItemService)
        {
            _createBasketItemService = createBasketItemService;
        }

        [HttpPost("addBasketItem")]
        public async Task<ActionResult<BasketItemDto>> CreateBasketItem([FromBody]BasketItemDto basketItem)
        {
            var result = await _createBasketItemService.CreateBasketItemAsync(basketItem);
            if (result.IsSuccess)
                return CreatedAtAction(nameof(CreateBasketItem), new { result.Value.BasketId }, result.Value);

            return BadRequest(result.Errors);
        }
    }
}
