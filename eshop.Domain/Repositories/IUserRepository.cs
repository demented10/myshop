
namespace eshop.Domain.Repositories
{
    public interface IUserRepository<User> : IRepository<User>
    {
        public Task<User> FindByEmailAsync(string email);
    }
}
