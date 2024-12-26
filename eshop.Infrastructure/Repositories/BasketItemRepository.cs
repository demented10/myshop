using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace eshop.Infrastructure.Repositories
{
    public class BasketItemRepository : IBasketItemRepository<BasketItem>
    {
        private readonly AppDbContext _context;

        public BasketItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(BasketItem entity)
        {
            await _context.BasketItems.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var basketItem = await _context.BasketItems.FindAsync(id);
            if (basketItem is null)
                return;
            _context.BasketItems.Remove(basketItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<BasketItem>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.BasketItems.ToListAsync();
        }

        public async Task<BasketItem> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var item = await _context.BasketItems.FirstOrDefaultAsync(t => t.Id == id);
            return item is null ? throw new ArgumentException($"Basket item with id {id} not found") : item;
        }

        public Task UpdateAsync(BasketItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
