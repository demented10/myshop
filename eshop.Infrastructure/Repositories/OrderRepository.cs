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
    public class OrderRepository : IOrderRepository<Order>
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Order entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Order>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(t => t.Id == id);
            return order is null ? throw new ArgumentException($"Order with id {id} not found.") : order;
        }

        public async Task<Order> GetOrderItemsAsync(int orderId)
        {
            var orders = await _context.Orders
                .Include(oi => oi.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);
              
            if (orders is null)
                throw new Exception("Order doesnt exists");

            return orders;
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(int userId)
        {
            var order = await _context.Orders.Where(t => t.UserId == userId).ToListAsync();
            return order;
        }

        public Task UpdateAsync(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
