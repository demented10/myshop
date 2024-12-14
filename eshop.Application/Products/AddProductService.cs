using eshop.Domain.Entities;
using eshop.Infrastructure;
using FluentResults;

namespace eshop.Application
{


    namespace eshop.Application.Products
    {
        public class AddProductService
        {
            private readonly RepositoryFactory _repositoryFactory;

            public AddProductService(RepositoryFactory repositoryFactory)
            {
                _repositoryFactory = repositoryFactory;
            }

            public async Task<Result<ProductDto>> AddItemAsync(Product item, CancellationToken cancellationToken)
            {
                var repository = _repositoryFactory.CreateProductRepository();

                try
                {
                    await repository.CreateAsync(item);
                    return Result.Ok(new ProductDto(  item.Id, item.Name,  item.Price));
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
