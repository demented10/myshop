using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eshop.Domain.Entities;
using eshop.Infrastructure;
using eshop.Application.eshop.Application.Products;
using eshop.Application.Categories;

namespace eshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly AddProductService _addService;
        private readonly GetProductService _getProductService;

        public ProductController(AddProductService addService, GetProductService getProductService)
        {
            _addService = addService;
            _getProductService = getProductService;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] ProductDto productDto)
        {
            var result = await _addService.AddItemAsync(productDto);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(Create), new { result.Value.Id }, result.Value);
            }

            return BadRequest(result.Errors);
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllAsync()
        {
            var result = await _getProductService.GetItemsAsync(CancellationToken.None);
            if (result.IsSuccess)
            {
                return Json(result.Value);
            }
            return BadRequest(result.Errors);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetByIdAsync(int id)
        {
            var result = await _getProductService.GetItemAsync(id, CancellationToken.None);
            if(result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }

        [HttpGet("{productId:int}/category")]
        public async Task<ActionResult<CategoryDto>> GetProductCategoryAsync(int productId)
        {
            var result = await _getProductService.GetProductCategoryAsync(productId, CancellationToken.None);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }
    }
}
