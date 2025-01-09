namespace eshop.Domain.Repositories
{
    public interface IOrderStatusRepository<OrderStatus> : IRepository<OrderStatus>
    {
        Task<OrderStatus> GetNotPayedStatusAsync();
    }
}
