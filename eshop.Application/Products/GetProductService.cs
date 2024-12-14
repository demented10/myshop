using eshop.Infrastructure;
using FluentResults;
using eshop.Domain.Entities;

namespace eshop.Application
{


    namespace eshop.Application.Products
    {
        public class GetProductService
        {
            private readonly RepositoryFactory _repositoryFactory;

            public GetProductService(RepositoryFactory repositoryFactory)
            {
                _repositoryFactory = repositoryFactory;
            }

            public async Task<Result<IEnumerable<ProductDto>>> GetItemsAsync(CancellationToken cancellationToken)
            {
                var repository = _repositoryFactory.CreateProductRepository();

                try
                {
                    var items = (await repository
                        .GetAllAsync(cancellationToken));

                    return Result.Ok(items
                        .Select(i => new ProductDto(i.Id, i.Name, i.Price)));
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
                var repository = _repositoryFactory.CreateProductRepository();
                try
                {
                    var item = await repository.GetByIdAsync(id, cancellationToken);
                    return Result.Ok(new ProductDto(item.Id, item.Name, item.Price));
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
