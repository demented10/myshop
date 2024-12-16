using eshop.Infrastructure;
using FluentResults;
using eshop.Domain.Entities;
using eshop.Infrastructure.Repositories;
using eshop.Application.Categories;
using eshop.Domain.Repositories;

namespace eshop.Application
{


    namespace eshop.Application.Products
    {
        public class GetProductService
        {
            private readonly IProductRepository<Product> _productRepository;

            public GetProductService(IProductRepository<Product> productRepository)
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
                        .Select(i => new ProductDto(i.Id, i.Name,i.Description, i.Price, i.CategoryId)));
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
                    return Result.Ok(new ProductDto(item.Id, item.Name, item.Description, item.Price, item.CategoryId));
                }
                catch (Exception ex)
                {
                    return Result.Fail("Не удалось получить товар")
                        .WithError(ex.Message)
                        .WithError(ex.StackTrace);
                }

            }
            public async Task<Result<CategoryDto>> GetProductCategoryAsync(int productId, CancellationToken cancellationToken)
            {
                try
                {
                    var item = await _productRepository.GetProductCategoryAsync(productId, cancellationToken);
                    return Result.Ok(new CategoryDto(item.Category.Id, item.Category.Name));
                }
                catch(Exception ex)
                {
                    return Result.Fail("Не удалось получить категорию для товара").WithError(ex.Message).WithError(ex.StackTrace);
                }
            }
        }
    }
}
