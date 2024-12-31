using eshop.Application.Categories;
using eshop.Application.eshop.Application.Products;
using eshop.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly AddCategoryService _addCategoryService;
        private readonly GetCategoryService _getCategoryService;
        public CategoryController(AddCategoryService addCategoryService, GetCategoryService getCategoryService) 
        {
            _addCategoryService = addCategoryService;
            _getCategoryService = getCategoryService;
        }


        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create([FromBody] CategoryDto categoryDto)
        {
            var result = await _addCategoryService.AddCategoryAsync(categoryDto);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(Create), new {result.Value.Id}, result.Value);
            }

            return BadRequest(result.Errors);

        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDto>> GetAsync(int id)
        {
            var result = await _getCategoryService.GetCategoryByIdAsync(id, CancellationToken.None);

            if (result.IsSuccess)
            {
                return Ok(result.Value); 
            }
            return BadRequest(result.Errors);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllAsync()
        {
            var result = await _getCategoryService.GetAllCategoriesAsync(CancellationToken.None);

            if(result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }

        [HttpGet("{id:int}/products")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProductsInCategoryAsync(int id)
        {
            var result = await _getCategoryService.GetAllProductsInCategoryAsync(id, CancellationToken.None);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
           
        }

    }
}
