using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Infrastructure.Repositories
{
    public interface IReadOnlyRepository<T>
    {
        public Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        public Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken = default);

    }
}
