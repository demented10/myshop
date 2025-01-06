using eshop.Application.BasketItems;
using eshop.Domain.Entities;
using eshop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.Marshalling;

namespace eshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketItemController : Controller
    {
        private readonly CreateBasketItemService _createBasketItemService;
        private readonly QuantityBasketItemService _quantityBasketItemService;
        public BasketItemController(CreateBasketItemService createBasketItemService, QuantityBasketItemService quantityBasketItemService)
        {
            _createBasketItemService = createBasketItemService;
            _quantityBasketItemService = quantityBasketItemService;
        }

        [HttpPost("addBasketItem")]
        public async Task<ActionResult<BasketItemDto>> CreateBasketItem([FromBody]BasketItemDto basketItem)
        {
            var result = await _createBasketItemService.CreateBasketItemAsync(basketItem);
            if (result.IsSuccess)
                return CreatedAtAction(nameof(CreateBasketItem), new { result.Value.BasketId }, result.Value);

            return BadRequest(result.Errors);
        }
        [HttpPost("addCount")]
        public async Task<ActionResult> AddCountBasketItem([FromBody]BasketItemRequest request)
        {
            var result = await _quantityBasketItemService.AddBasketItemCount(request.basketId, request.productId, request.count);
            if (result.IsSuccess)
                return Ok();

            return BadRequest(result.Errors);
        }
        [HttpPost("removeCount")]
        public async Task<ActionResult> RemoveCountBasketItem([FromBody] BasketItemRequest request)
        {
            var result = await _quantityBasketItemService.RemoveBasketItemCount(request.basketId, request.productId, request.count);
            if (result.IsSuccess)
                return Ok();

            return BadRequest(result.Errors);
        }
    }
}
