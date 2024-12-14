using eshop.Infrastructure;
using FluentResults;
using eshop.Domain.Entities;
using eshop.Infrastructure.Repositories;

namespace eshop.Application
{


    namespace eshop.Application.Products
    {
        public class GetProductService
        {
            private readonly IRepository<Product> _productRepository;

            public GetProductService(IRepository<Product> productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<Result<IEnumerable<ProductDto>>> GetItemsAsync(CancellationToken cancellationToken)
            {
                try
                {
                    var items = (await _productRepository
                        .GetAllAsync(cancellationToken));

                    return Result.Ok(items
                        .Select(i => new ProductDto(i.Id, i.Name, i.Price, i.Category.Id)));
                }
                catch (Exception ex)
                {
                    return Result.Fail("Не удалось получить коллекцию торговых единиц")
                        .WithError(ex.Message)
                        .WithError(ex.StackTrace);
                }
            }
            public async Task<Result<ProductDto>> GetItemAsync(int id, CancellationToken cancellationToken)
            {
                try
                {
                    var item = await _productRepository.GetByIdAsync(id, cancellationToken);
                    return Result.Ok(new ProductDto(item.Id, item.Name, item.Price, item.Category.Id));
                }
                catch (Exception ex)
                {
                    return Result.Fail("Не удалось получить товар")
                        .WithError(ex.Message)
                        .WithError(ex.StackTrace);
                }


            }
        }
    }


   
    
}
