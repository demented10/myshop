using Castle.Core.Logging;
using eshop.Application.OrderItems;
using eshop.Domain;
using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using eshop.Infrastructure;
using FluentResults;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Application.Orders
{
    public class CreateOrderService(IOrderRepository<Order> orderRepository, 
        IOrderStatusRepository<OrderStatus> orderStatusRepository,
        IBasketRepository<Basket> basketRepository,
        ILogger<CreateOrderService> logger,
        AppDbContext context, CreateOrderItemService createOrderItemService)
    {
        private readonly IOrderRepository<Order> _orderRepository = orderRepository;
        private readonly ILogger<CreateOrderService> _logger = logger;
        private readonly AppDbContext _context = context;
        private readonly IBasketRepository<Basket> _basketRepository = basketRepository;
        private readonly IOrderStatusRepository<OrderStatus> _orderStatusRepository = orderStatusRepository;
        private readonly CreateOrderItemService _createOrderItemService = createOrderItemService;

        public async Task<Result> CreateOrderAsync(int userId, CancellationToken cancellationToken)
        {
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {

                    var userBaskets = await _basketRepository.GetBasketsByUserIdAsync(userId);
                    
                    //Получаем корзину
                    if (userBaskets is null)
                        throw new ArgumentNullException("User basket not found!");

                    var currentBasket = userBaskets.OrderByDescending(item => item.Id).FirstOrDefault();
                    //Формируем заказ
                    if (currentBasket is null)
                        throw new ArgumentNullException("Current basket not found!");

                    var newOrder = CreateOrderFromBasket(currentBasket, 
                        (await _orderStatusRepository.GetNotPayedStatusAsync()).Id);
                    await _orderRepository.CreateAsync(newOrder);
                    //Заполняем заказ товарами из корзины


                    await _createOrderItemService.CreateOrderItemsFromBasketItems(newOrder, currentBasket.BasketItems);

                    //Бронируем предметы на складе
                    //--в процессе--

                    transaction.Commit();

                    return Result.Ok();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError(ex, "Ошибка при создании заказа");
                    return Result.Fail("Не удалось создать корзину").WithError(ex.Message).WithError(ex.StackTrace);
                }
            }
        }

        private static Order CreateOrderFromBasket(Basket basket, int statusId)
        {
            if (basket.BasketItems is null)
                throw new ArgumentNullException("BasketItems is null");
            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                TotalAmount = OrderCalculator.CalculateTotalCost(basket.BasketItems),
                UserId = basket.UserId,
                StatusId = statusId             
            };
            return order;
        }

    }
}
