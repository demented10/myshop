using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using FluentResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Application.BasketItems
{
    public class QuantityBasketItemService
    {
        private readonly IBasketItemRepository<BasketItem> _basketItemRepository;
        private readonly ILogger _logger;

        public QuantityBasketItemService(IBasketItemRepository<BasketItem> basketItemRepository, ILoggerProvider logger)
        {
            _basketItemRepository = basketItemRepository;
            _logger = logger.CreateLogger("");
        }
        public async Task<Result> AddBasketItemCount(int basketId, int productId, int count)
        {
            try
            {
                var basketItem = await _basketItemRepository.GetByIdAsync(basketId, productId);

                if (basketItem == null)
                {
                    throw new ArgumentException("Запись не найдена");
                }

                await _basketItemRepository.ChangeQuantityAsync(basketId, productId, basketItem.Quantity + count);
                return Result.Ok();
            }
            catch (ArgumentException)
            {
                var newBasketItem = new BasketItem
                {
                    BasketId = basketId,
                    ProductId = productId,
                    Quantity = count
                };
                await _basketItemRepository.CreateAsync(newBasketItem);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                // Логирование исключения для дальнейшей диагностики
                _logger.LogError(ex, "Ошибка при изменении количества товара в корзине.");
                return Result.Fail($"Не удалось добавить количество к товару в корзине: {ex.Message}");
            }
        }
        public async Task<Result> RemoveBasketItemCount(int basketId, int productId, int count)
        {
            try
            {
                var basketItem = await _basketItemRepository.GetByIdAsync(basketId, productId);
                if (basketItem is null)
                    return Result.Fail("Not found basketItem");
                if (basketItem.Quantity - count <= 0)
                {
                    await _basketItemRepository.DeleteAsync(basketId, productId);
                    return Result.Ok();
                }
                await _basketItemRepository.ChangeQuantityAsync(basketId, productId, basketItem.Quantity - count);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail("Не удалось убавить количество товара в корзине ");
            }
        }
    }
}
