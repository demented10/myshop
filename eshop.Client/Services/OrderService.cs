using Blazored.LocalStorage;
using eshop.Client.Models.Baskets;
using eshop.Client.Models.Orders;
using eshop.Client.Models.Products;
using System.Net.Http.Json;

namespace eshop.Client.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;
        private readonly CustomAuthenticationStateProvider _authStateProvider;

        public OrderService(HttpClient httpClient, CustomAuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
        }

        public async Task<bool> CreateOrderAsync(string userId)
        {
            if (await AuthHelper.IsAuth(_authStateProvider))
            {
                var result = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/Order/createUserOrder/{userId}", null);
                return result.IsSuccessStatusCode;
            }
            return false;
       }
        public async Task<IEnumerable<Order>> GetUserOrders(string userId)
        {
            if (await AuthHelper.IsAuth(_authStateProvider))
            {
                var result = await _httpClient.GetFromJsonAsync<IEnumerable<OrderResponse>>($"{_httpClient.BaseAddress}/Order/getUserOrders/{userId}");
                if (result is not null)
                {
                    List<Order> orders = new();
                    foreach (var o in result)
                    {
                        var orderItemsResult = await _httpClient.GetFromJsonAsync<IEnumerable<OrderItemResponse>>($"{_httpClient.BaseAddress}/Order/{o.Id}/getOrderItems");
                        if (orderItemsResult is null)
                            continue;
                        List<OrderItem> orderItems = new();
                        decimal orderTotalSum = 0;
                        foreach(var oi in orderItemsResult)
                        {
                            #region Вынести в сервис товаров
                            var productReponse = await _httpClient.GetFromJsonAsync<ProductResponse>($"{_httpClient.BaseAddress}/Product/{oi.ProductId}");
                                orderItems.Add(new OrderItem
                                {
                                    product = new Product { Id = productReponse.Id, Description = productReponse.Description, Name = productReponse.Name, Price = productReponse.Price },
                                    Quantity = oi.Quantity,
                                    TotalAmount = oi.UnitPrice
                                });
                            #endregion

                            orderTotalSum += oi.UnitPrice;
                        }
                        orders.Add(new Order
                        {
                            OrderId = o.Id,
                            UserId = o.UserId,
                            OrderItems = orderItems,
                            OrderTotalAmount = orderTotalSum
                        });
                    }
                    return orders;
                }
            }
            return Enumerable.Empty<Order>();
        }
    }
}