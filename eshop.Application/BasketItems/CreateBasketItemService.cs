using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using FluentResults;

namespace eshop.Application.BasketItems
{
    public class CreateBasketItemService
    {
        private readonly IBasketItemRepository<BasketItem> _basketItemRepository;

        public CreateBasketItemService(IBasketItemRepository<BasketItem> basketItemRepository)
        {
            _basketItemRepository = basketItemRepository;
        }

        public async Task<Result<BasketItemDto>> CreateBasketItemAsync(BasketItemDto basketItemDto)
        {
            try
            {
                var basketItem = new BasketItem
                {
                    BasketId = basketItemDto.BasketId,
                    ProductId = basketItemDto.ProductId,
                    Quantity = basketItemDto.Quantity
                };
                await _basketItemRepository.CreateAsync(basketItem);
                return Result.Ok(basketItemDto);
            }
            catch(Exception ex)
            {
                return Result.Fail($"Не удалось добавить предмет в корзину с id {basketItemDto.BasketId}, с id товара {basketItemDto.ProductId}")
                    .WithError(ex.Message)
                    .WithError(ex.StackTrace);
            }
        }
    }
}
