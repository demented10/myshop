
using eshop.Domain.Entities;

namespace eshop.Infrastructure.Repositories
{
    public class DatabaseRepositoryFactory : RepositoryFactory
    {
        private readonly AppDbContext _appDbContext;
        public DatabaseRepositoryFactory(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
       // public override IReadOnlyRepository<Product> CreateReadOnlyProductRepository()
       // {
       //     return new ProductRepository(_appDbContext);
        //}
        public override IRepository<Product> CreateProductRepository()
        {
            return new ProductRepository(_appDbContext);
        }

        public override IRepository<Category> CreateCategoryRepository()
        {
            return new CategoryRepository(_appDbContext);
        }
    }
}
