using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eshop.Domain.Entities;
using eshop.Infrastructure;
using eshop.Application.eshop.Application.Products;

namespace eshop.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<ActionResult<ProductDto>> Create([FromBody] Product product)
        {
            var result = await _addService.AddItemAsync(product, CancellationToken.None);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(Create), new { result.Value.id }, result.Value);
            }

            return BadRequest(result.Errors);
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllAsync()
        {
            var result = await _getProductService.GetItemsAsync(CancellationToken.None);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
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

        // Другие методы...
    }
}
