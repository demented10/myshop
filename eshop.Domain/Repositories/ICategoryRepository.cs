using eshop.Domain.Entities;
using eshop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Domain.Repositories
{
    public interface ICategoryRepository<Category> : IRepository<Category>
    {
        public Task<Category> GetCategoryWithProductsAsync(int categoryId, CancellationToken cancellationToken);
    }
}
