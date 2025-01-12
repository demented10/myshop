using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace eshop.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User entity)
        {
            try
            {
                await _context.Users.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                if(ex.InnerException is PostgresException pgEx &&
                    (pgEx.SqlState == PostgresErrorCodes.UniqueViolation))
                {
                    throw new ArgumentException($"User with name {entity.Name} or email {entity.Email} is exists");
                }

                throw;
            }
        }

        public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            
            var user = await _context.Users.FindAsync(id,CancellationToken.None);
            return user is null ? throw new ArgumentException($"User with id {id} not found.") : user;
        }
        public async Task<User> FindByEmailAsync(string email)
        {
          var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
          return user;
        }
        public async Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }

       
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        
    }
}
