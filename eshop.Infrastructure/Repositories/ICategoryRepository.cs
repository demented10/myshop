using eshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Infrastructure.Repositories
{
    public interface ICategoryRepository<Category>
    {
        public Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        public Task<IReadOnlyCollection<Category>> GetAllAsync(CancellationToken cancellationToken = default);
        public Task CreateAsync(Category entity);
        public Task UpdateAsync(Category entity);
        public Task DeleteAsync(int id);
        public Task<Category> GetCategoryWithProductsAsync(int categoryId, CancellationToken cancellationToken);
    }
}
