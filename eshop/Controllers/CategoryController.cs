using eshop.Application.Categories;
using eshop.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eshop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly AddCategoryService _addCategoryService;

        public CategoryController(AddCategoryService addCategoryService) => _addCategoryService = addCategoryService;


        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create([FromBody] CategoryDto categoryDto)
        {
            var result = await _addCategoryService.AddCategoryAsync(categoryDto, CancellationToken.None);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(Create), new {result.Value.id}, result.Value);
            }

            return BadRequest(result.Errors);

        }

    }
}
