using eshop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Domain.Repositories
{
    public interface IProductRepository<Product> : IRepository<Product>
    {
        public Task<Product> GetProductCategoryAsync(int productId, CancellationToken cancellationToken);
    }
}
