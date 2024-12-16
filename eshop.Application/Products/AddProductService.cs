using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using eshop.Infrastructure;
using eshop.Infrastructure.Repositories;
using FluentResults;

namespace eshop.Application
{


    namespace eshop.Application.Products
    {
        public class AddProductService
        {
            private readonly IProductRepository<Product> _productRepository;

            public AddProductService(IProductRepository<Product> productRepository)
            {
               _productRepository = productRepository;
            }

            public async Task<Result<ProductDto>> AddItemAsync(ProductDto productDto)
            {
                try
                {
                    var product = new Product{
                        Name = productDto.name,
                        Price = productDto.price,
                        Description = productDto.description,
                        CategoryId = productDto.categoryId
                    };
                    await _productRepository.CreateAsync(product);
                    return Result.Ok(new ProductDto(product.Id, product.Name, product.Description, product.Price, product.CategoryId));
                }
                catch (Exception ex)
                {
                    return Result.Fail<ProductDto>("Не удалось добавить товар")
                        .WithError(ex.Message)
                        .WithError(ex.StackTrace);
                }
            }
        }
    }


   
    
}
