using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FindAsync(id);
            return user is null ? throw new ArgumentException($"User with id {id} not found.") : user;
        }

        public async Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Users.ToListAsync();
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
