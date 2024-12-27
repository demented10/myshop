using eshop.Application.Users;
using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using FluentResults;
using Npgsql.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Application.Baskets
{
    public class GetBasketService
    {
        private readonly IBasketRepository<Basket> _basketRepository;

        public GetBasketService(IBasketRepository<Basket> basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Result<BasketDto>> GetBasketByIdAsync(int id)
        {
            try
            {
                var basket = await _basketRepository.GetByIdAsync(id);
                return Result.Ok(new BasketDto(basket.Id, basket.UserId));

            }
            catch (Exception ex)
            {
                return Result.Fail($"Не удалось получить корзину с id {id}")
                    .WithError(ex.Message)
                    .WithError(ex.StackTrace);
            }
        }
        public async Task<Result<IEnumerable<BasketDto>>> GetAllBasketsAsync()
        {
            try
            {
                var items = await _basketRepository.GetAllAsync();

                return Result.Ok(items.Select(i => new BasketDto(i.Id, i.UserId)));

            }
            catch(Exception ex)
            {
                return Result.Fail("Не удалось получить список корзин").WithError(ex.Message).WithError(ex.StackTrace);
            }
        }
        public async Task<Result<IEnumerable<BasketDto>>> GetBasketsByUserAsync(int userId)
        {
            try
            {
                var items = await _basketRepository.GetBasketsByUserIdAsync(userId);

                return Result.Ok(items.Select(i=> new BasketDto(i.Id, i.UserId)));
            }
            catch(Exception ex)
            {
                return Result.Fail($"Не удалось получить корзины для пользователя с id {userId}").WithError(ex.Message).WithError(ex.StackTrace);
            }
        }
    }
}
