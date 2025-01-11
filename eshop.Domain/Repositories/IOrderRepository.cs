
namespace eshop.Domain.Repositories
{
    public interface IOrderRepository<Order> : IRepository<Order>
    {
        public Task<Order> GetOrderItemsAsync(int orderId);
        public Task<IEnumerable<Order>> GetUserOrdersAsync(int userId);
    }
}
