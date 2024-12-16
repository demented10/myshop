using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Infrastructure.Repositories
{
    public interface IRepository<T> : IReadOnlyRepository<T>
    {
       public Task CreateAsync(T entity);
       public Task UpdateAsync(T entity);
       public Task DeleteAsync(int id);
    }
}
