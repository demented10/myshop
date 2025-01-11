using eshop.Application.BasketItems;
using eshop.Application.OrderItems;
using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using eshop.Infrastructure.Repositories;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Application.Orders
{
    public class GetOrderService
    {
        private readonly IOrderRepository<Order> _orderRepository;
        public GetOrderService(IOrderRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<IEnumerable<OrderDto>>> GetAllOrdersAsync()
        {
            try
            {
                var items = await _orderRepository.GetAllAsync();

                return Result.Ok(items.Select(i => new OrderDto(i.Id, 
                    i.UserId, 
                    i.OrderDate, 
                    i.Status.ToString(), 
                    i.TotalAmount)));
            }
            catch(Exception ex)
            {
                return Result.Fail("Не удалось получить все заказы")
                    .WithError(ex.Message)
                    .WithError(ex.StackTrace);
            }
        } 
        public async Task<Result<OrderDto>> GetOrderByIdAsync(int id)
        {
            try
            {
                var item = await _orderRepository.GetByIdAsync(id);
                return Result.Ok(new OrderDto(item.Id, item.UserId, item.OrderDate, item.Status.ToString(), item.TotalAmount));
            }
            catch (Exception ex)
            {
                return Result.Fail($"Не удалось получить корзину с id {id}").WithError(ex.Message).WithError(ex.StackTrace);
            }
        }
        public async Task<Result<IEnumerable<OrderItemDto>>> GetAllOrderItemsAsync(int orderId)
        {
            try
            {
                var items = await _orderRepository.GetOrderItemsAsync(orderId);
                if (items.OrderItems is null)
                    throw new ArgumentNullException($"In order with Id {orderId} orderItems is null");
                return Result.Ok(items.OrderItems.Select(i => new OrderItemDto(i.OrderId, i.ProductId, i.Quantity, i.UnitPrice)));
            }
            catch (Exception ex)
            {
                return Result.Fail($"Не удалось получить предметы заказа с id {orderId}").WithError(ex.Message).WithError(ex.StackTrace);
            }
        }
        public async Task<Result<IEnumerable<OrderDto>>> GetAllUserOrdersAsync(int userId)
        {
            try
            {
                var items = await _orderRepository.GetUserOrdersAsync(userId);
                return Result.Ok(items.Select(i => new OrderDto(i.Id, i.UserId, i.OrderDate, i.Status.ToString(), i.TotalAmount)));
            }
            catch(Exception ex)
            {
                return Result.Fail($"Не удалось получить заказы пользователя с id {userId}")
                    .WithError(ex.Message)
                    .WithError(ex.StackTrace);
            }
        }
    }
}
