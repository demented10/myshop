using eshop.Domain.Entities;
using eshop.Infrastructure.Repositories;


namespace eshop.Infrastructure
{
    public abstract class RepositoryFactory
    {
       // public abstract IReadOnlyRepository<Product> CreateReadOnlyProductRepository();
        public abstract IRepository<Product> CreateProductRepository();

        public abstract IRepository<Category> CreateCategoryRepository();
    }
}
