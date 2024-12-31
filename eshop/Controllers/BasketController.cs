using eshop.Application.BasketItems;
using eshop.Application.Baskets;
using eshop.Application.Users;
using Microsoft.AspNetCore.Mvc;

namespace eshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        private readonly CreateBasketService _createBasketService;
        private readonly GetBasketService _getBasketService;
        private readonly DeleteBasketService _deleteBasketService;
        public BasketController(CreateBasketService createBasketService, 
            GetBasketService getBasketService, 
            DeleteBasketService deleteBasketService)
        {
            _createBasketService = createBasketService;
            _getBasketService = getBasketService;
            _deleteBasketService = deleteBasketService;
        }

        [HttpPost("createBasket/{userId:int}")]
        public async Task<ActionResult<BasketDto>> CreateBasket(int userId)
        {
            var result = await _createBasketService.CreateBasketForUserAsync(userId);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(CreateBasket), new { result.Value.BasketId}, result.Value);
            }

            return BadRequest(result.Errors);

        }
        [HttpGet("getBasket/{id:int}")]
        public async Task<ActionResult<BasketDto>> GetBasket(int id)
        {
            var result = await _getBasketService.GetBasketByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Errors);
        }
        [HttpGet("getBaskets")]
        public async Task<ActionResult<IEnumerable<BasketDto>>> GetBaskets()
        {
            var result = await _getBasketService.GetAllBasketsAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }
        [HttpGet("getUserBaskets/{userId:int}")]
        public async Task<ActionResult<IEnumerable<BasketDto>>> GetUserBaskets(int userId)
        {
            var result = await _getBasketService.GetBasketsByUserAsync(userId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }
        [HttpGet("{basketId:int}/getBasketItems")]
        public async Task<ActionResult<IEnumerable<BasketItemDto>>> GetBasketItems(int basketId)
        {
            var result = await _getBasketService.GetAllBasketItemsAsync(basketId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("deleteBasket")]
        public async Task<ActionResult> deleteBasketById([FromBody]int basketId)
        {
            var result = await _deleteBasketService.DeleteBasketAsync(basketId);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result.Errors);
        }

    }    
}
