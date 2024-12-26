
namespace eshop.Domain.Repositories
{
    public interface IProductRepository<Product> : IRepository<Product>
    {
        public Task<Product> GetProductCategoryAsync(int productId, CancellationToken cancellationToken);
    }
}
