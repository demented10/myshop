using eshop.Domain.Entities;

namespace eshop.Domain.Repositories
{
    public interface IOrderItemRepository<OrderItem> : IRepository<OrderItem>
    {
        Task CreateRangeAsync(IEnumerable<OrderItem> orderItems);
    }
}
