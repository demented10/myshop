
namespace eshop.Domain.Repositories
{
    public interface ICategoryRepository<Category> : IRepository<Category>
    {
        public Task<Category> GetCategoryWithProductsAsync(int categoryId, CancellationToken cancellationToken);
    }
}
