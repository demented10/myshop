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
    public class OrderItemRepository : IOrderItemRepository<OrderItem>
    {
        private readonly AppDbContext _context;

        public OrderItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(OrderItem entity)
        {
            await _context.OrderItems.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task CreateRangeAsync(IEnumerable<OrderItem> orderItems)
        {
            await _context.OrderItems.AddRangeAsync(orderItems);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<OrderItem>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.OrderItems.ToListAsync();
        }

        public Task<OrderItem> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(OrderItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
