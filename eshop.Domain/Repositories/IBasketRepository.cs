﻿
using eshop.Domain.Entities;

namespace eshop.Domain.Repositories
{
    public interface IBasketRepository<Basket> : IRepository<Basket>
    {
        public Task<IReadOnlyCollection<Basket>> GetBasketsByUserIdAsync(int userId);
        public Task<IReadOnlyCollection<BasketItem>> GetBasketItemsAsync(int basketId);
    }
}
