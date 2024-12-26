
using eshop.Domain.Entities;

namespace eshop.Domain.Repositories
{
    public interface IBasketRepository<Basket> : IRepository<Basket>
    {
        public Task<Basket> GetBasketByUserIdAsync(int userId);
        public Task<IReadOnlyCollection<BasketItem>> GetBasketItemsAsync(int basketId);
    }
}
