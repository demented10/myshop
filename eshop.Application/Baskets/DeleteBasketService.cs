using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using FluentResults;

namespace eshop.Application.Baskets
{
    public class DeleteBasketService
    {
        private readonly IBasketRepository<Basket> _basketRepository;

        public DeleteBasketService(IBasketRepository<Basket> basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Result> DeleteBasketAsync(int basketId)
        {
            try
            {
                await _basketRepository.DeleteAsync(basketId);
                return Result.Ok();
            }
            catch(Exception ex)
            {
                return Result.Fail($"Не удалось удалить корзину с id {basketId}")
                    .WithError(ex.Message)
                    .WithError(ex.StackTrace);
            }
        }

    }
}
