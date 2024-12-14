using eshop.Domain.Entities;
using eshop.Infrastructure;
using eshop.Infrastructure.Repositories;
using FluentResults;

namespace eshop.Application
{


    namespace eshop.Application.Products
    {
        public class AddProductService
        {
            private readonly IRepository<Product> _productRepository;

            public AddProductService(IRepository<Product> productRepository)
            {
               _productRepository = productRepository;
            }

            public async Task<Result<ProductDto>> AddItemAsync(Product item, CancellationToken cancellationToken)
            {
                var repository = _productRepository;

                try
                {
                    await repository.CreateAsync(item);
                    return Result.Ok(new ProductDto(  item.Id, item.Name,  item.Price, item.Category.Id));
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
