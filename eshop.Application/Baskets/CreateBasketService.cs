
using eshop.Application.Users;
using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Application.Baskets
{
    public class CreateBasketService 
    {
        private readonly IBasketRepository<Basket> _basketRepository;

        public CreateBasketService(IBasketRepository<Basket> basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Result<BasketDto>> CreateBasketForUserAsync(int userId)
        {
            try
            {
                var basket = new Basket { UserId = userId };
                await _basketRepository.CreateAsync(basket);

                return Result.Ok(new BasketDto (basket.Id, basket.UserId));
            }
            catch(Exception ex)
            {
                return Result.Fail("Не удалось создать корзину")
                    .WithError(ex.Message)
                    .WithError(ex.StackTrace);
            }
        }


    }
}
