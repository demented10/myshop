using Blazored.LocalStorage;
using Blazored.LocalStorage.StorageOptions;
using eshop.Client.Models;
using eshop.Client.Models.Basket;
using Microsoft.AspNetCore.Components.Authorization;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text.Json;

namespace eshop.Client.Services
{
    public class BasketService
    {
        const string BASKET_KEY = "basket";
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly CustomAuthenticationStateProvider _authStateProvider;
        public BasketService(HttpClient httpClient, 
        ILocalStorageService localStorage, 
        CustomAuthenticationStateProvider authStateProvider){
            _httpClient = httpClient;
            _localStorage =  localStorage;
            _authStateProvider = authStateProvider; 
        }

        public async Task<Basket> GetBasketAsync()
        {
            var basket = await  _localStorage.GetItemAsync<Basket>(BASKET_KEY) ?? new Basket();


            if (await AuthHelper.IsAuth(_authStateProvider))
            {
                var serverBasket = await _httpClient.GetFromJsonAsync<BasketResponse>($"{_httpClient.BaseAddress}/Basket/getCurrentUserBasket/{_authStateProvider.CurrentUser.UserId}");
                var serverBasketItems = await _httpClient.GetFromJsonAsync<IEnumerable<BasketItemResponse>>($"{_httpClient.BaseAddress}/Basket/{serverBasket.BasketId}/getBasketItems");
                if (serverBasketItems is not null)
                {
                    var basketItems = new List<BasketItem>();

                    foreach(var item in serverBasketItems)
                    {
                        var productReponse = await _httpClient.GetFromJsonAsync<ProductResponse>($"{_httpClient.BaseAddress}/Product/{item.ProductId}");
                        var basketItem = new BasketItem
                        {
                            product = new Product { Id = productReponse.Id, Description = productReponse.Description, Name = productReponse.Name, Price = productReponse.Price },
                            Quantity = item.Quantity
                        };
                        basketItems.Add(basketItem);
                    }

                    var userBasket = new Basket
                    {
                        BasketId = serverBasket.BasketId,
                        UserId = serverBasket.UserId,
                        BasketItems = basketItems
                    };
                    return await MergeBaskets(basket, userBasket);
                }
            }
            return basket;
        }
        public async Task<Basket> MergeBaskets(Basket serverBasket, Basket clientBasket)
        {
            var isAuth = await AuthHelper.IsAuth(_authStateProvider);

            foreach (var item in serverBasket.BasketItems)
            {
                if (!clientBasket.BasketItems.Any(i => i.product.Id == item.product.Id))
                {
                    clientBasket.BasketItems.Add(item);
                }
                if (isAuth)
                {
                    await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/BasketItem/addBasketItem", new
                    {
                        BasketId = serverBasket.BasketId,
                        ProductId = item.product.Id,
                        Quantity = item.Quantity
                    });
                }
            }
            return clientBasket;
        }
        public async Task AddBasketItemAsync(BasketItem basketItem)
        {
            var basket = await GetBasketAsync();
            var authState = await _authStateProvider.GetAuthenticationStateAsync();

            if (await AuthHelper.IsAuth(_authStateProvider))
            {
                var userId = _authStateProvider.CurrentUser.UserId;
                await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/BasketItem/addCount", 
                    new { basketId = basket.BasketId, productId = basketItem.product.Id, count = basketItem.Quantity });
            }
            else
            {
                if (basket.BasketItems.Any(bi => bi.product.Id == basketItem.product.Id))
                {
                    basket.BasketItems.FirstOrDefault(bi => basketItem.product.Id == bi.product.Id).Quantity += basketItem.Quantity;
                }
                else
                {
                    basket.BasketItems.Add(basketItem);
                }
            }
            await SaveBasketAsync(basket);
        }
        private async Task SaveBasketAsync(Basket basket)
        {           
            await _localStorage.SetItemAsync(BASKET_KEY, basket);        
        }
        public async Task RemoveItemAsync(int productId)
        {
            var basket = await GetBasketAsync();
            if(await AuthHelper.IsAuth(_authStateProvider))
            {
                await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/BasketItem/removeItem", new { basketId = basket.BasketId, productId = productId });                 
            }
            basket.BasketItems.RemoveAll(t=>t.product.Id == productId);
            
            await SaveBasketAsync(basket);
        }
        public async Task ClearBasketAsync()
        {
            var basket = await GetBasketAsync();
            basket.BasketItems.Clear();
            await SaveBasketAsync(basket);
        }
        public async Task<int> ChangeItemCount(int count, int productId)
        {
            var basket = await GetBasketAsync();
            var item = basket.BasketItems.FirstOrDefault(bi => bi.product.Id == productId);
            if (item is null) return -1;
            if (await AuthHelper.IsAuth(_authStateProvider))
            {
                var userId = _authStateProvider.CurrentUser.UserId;
                if (count >= 0)
                {
                    await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/BasketItem/addCount",
                    new { basketId = basket.BasketId, productId, count });
                }
                else
                {
                    var reverseCount = -count;
                    await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/BasketItem/removeCount",
                    new { basketId = basket.BasketId, productId, count = reverseCount });

                }
            }

            item.Quantity += count;
            if (item.Quantity <= 0)
                basket.BasketItems.Remove(item);
            await SaveBasketAsync(basket);
            return item.Quantity;

        }

    }
}