
namespace eshop.Domain.Repositories
{
    public interface IRepository<T> : IReadOnlyRepository<T>
    {
       public Task CreateAsync(T entity);
       public Task UpdateAsync(T entity);
       public Task DeleteAsync(int id);
    }
}
