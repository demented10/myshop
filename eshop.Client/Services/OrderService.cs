using Blazored.LocalStorage;

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



    }
}
