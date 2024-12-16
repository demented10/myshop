using eshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Infrastructure.Repositories
{
    public interface ICategoryRepository<Category> : IRepository<Category>
    {
        public Task<Category> GetCategoryWithProductsAsync(int categoryId, CancellationToken cancellationToken);
    }
}
