using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Application.BasketItems
{
    public class RemoveBasketItemService
    {
        private readonly IBasketItemRepository<BasketItem> _basketItemRepository;

        public RemoveBasketItemService(IBasketItemRepository<BasketItem> basketItemRepository)
        {
            _basketItemRepository = basketItemRepository;
        }

        public async Task<Result<BasketItemDto>> RemoveBasketItemAsync(int basketId, int productId)
        {
            try
            {
                await _basketItemRepository.DeleteAsync(basketId,productId);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail($"Не удалось удалить предмет с id корзины {basketId}, с id товара {productId}")
                    .WithError(ex.Message)
                    .WithError(ex.StackTrace);
            }
        }
    }
}
