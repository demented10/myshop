using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Application.OrderItems
{
    public class CreateOrderItemService
    {
        private readonly IOrderItemRepository<OrderItem> _orderItemRepository;

        public CreateOrderItemService(IOrderItemRepository<OrderItem> orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }
        internal async Task<IEnumerable<OrderItem>> CreateOrderItemsFromBasketItems(Order order, IEnumerable<BasketItem> basketItems)
        {   
            var orderItems = basketItems.Select(bi => new OrderItem
            {
                OrderId = order.Id,
                ProductId = bi.ProductId,
                Quantity = bi.Quantity,
                UnitPrice = bi.Product.Price 
            });
            await _orderItemRepository.CreateRangeAsync(orderItems);
            return orderItems;

        } 
    }
}
