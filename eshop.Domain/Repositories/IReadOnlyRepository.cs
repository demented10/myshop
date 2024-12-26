
namespace eshop.Domain.Repositories
{
    public interface IReadOnlyRepository<T>
    {
        public Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        public Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken = default);

    }
}
