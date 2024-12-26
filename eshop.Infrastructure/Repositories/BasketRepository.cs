using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace eshop.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository<Basket>
    {
        private readonly AppDbContext _context;

        public BasketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Basket entity)
        {
            await _context.Baskets.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var basket = await _context.Baskets.FindAsync(id);
            if (basket is null) return;

            _context.Baskets.Remove(basket);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<Basket>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Baskets.ToListAsync();
        }

        public async Task<Basket> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var basket = await _context.Baskets.FirstOrDefaultAsync(t => t.Id == id);
            return basket is null ? throw new ArgumentException($"Basket with id {id} not found.") : basket;
        }

        public async Task UpdateAsync(Basket entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Basket> GetBasketByUserIdAsync(int userId)
        {
            var basket = await _context.Baskets.FirstOrDefaultAsync(t => t.UserId == userId);
            return basket is null ? throw new ArgumentException($"Basket with UserId {userId} not found.") : basket;
        }

        public async Task<IReadOnlyCollection<BasketItem>> GetBasketItemsAsync(int basketId)
        {
            throw new NotImplementedException();
        }
    }
}
