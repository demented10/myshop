using eshop.Application.Baskets;
using eshop.Application.Users;
using Microsoft.AspNetCore.Mvc;

namespace eshop.Controllers
{
    [Route("[controller]")]
    public class BasketController : Controller
    {
        private readonly CreateBasketService _createBasketService;

        public BasketController(CreateBasketService createBasketService)
        {
            _createBasketService = createBasketService;
        }

        [HttpPost("CreateBasket/{userId:int}")]
        public async Task<ActionResult<BasketDto>> CreateBasket(int userId)
        {
            var result = await _createBasketService.CreateBasketForUserAsync(userId);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(CreateBasket), new { result.Value.BasketId}, result.Value);
            }

            return BadRequest(result.Errors);

        }

    }    
}
