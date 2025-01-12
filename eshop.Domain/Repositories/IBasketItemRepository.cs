
namespace eshop.Domain.Repositories
{
   public interface IBasketItemRepository<BasketItem> : IRepository<BasketItem>
    {
        Task<BasketItem> ChangeQuantityAsync(int basketId, int productId, int quantity);
        Task<BasketItem> GetByIdAsync(int basketId, int productId, CancellationToken cancellationToken = default);
        Task DeleteAsync(int basketId, int productId); 
    }
}
