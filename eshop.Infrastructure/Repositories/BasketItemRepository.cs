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


        public async Task DeleteAsync(int basketId, int productId)
        {
            var basketItem = await _context.BasketItems.FindAsync(basketId, productId);
            if (basketItem is null)
                return;
            _context.BasketItems.Remove(basketItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<BasketItem>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.BasketItems.ToListAsync();
        }

        public async Task<BasketItem> GetByIdAsync(int basketId, int productId, CancellationToken cancellationToken = default)
        {
            var item = await _context.BasketItems.FindAsync(basketId, productId);
            return item is null ? throw new ArgumentException($"Basket item with id {basketId} not found") : item;
        }

      
        public async Task<BasketItem> ChangeQuantityAsync(int basketId, int productId, int quantity)
        {
            var basketItem = await _context.BasketItems.FindAsync(basketId, productId);
            if (basketItem is not null)
                basketItem.Quantity = quantity;
            await _context.SaveChangesAsync();
            return basketItem is null? throw new ArgumentException($"Basket item with id {basketId} not found") : basketItem;
        }

        public Task<BasketItem> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task UpdateAsync(BasketItem entity)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
