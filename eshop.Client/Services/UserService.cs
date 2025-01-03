using Blazored.LocalStorage;
using eshop.Client.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace eshop.Client.Services
{
    public class UserService
    {
        const string AUTH_KEY = "jwt";
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public UserService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<User?> SendAuthenticateRequestAsync(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress+"/Auth/auth", new { email = username, password = password });
            string result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var authResult = JsonSerializer.Deserialize<AuthResponse>(result);
                var claimPrincipal = CreateClaimsPrincipalFromToken(authResult.token);
                var user = User.FromClaimsPrincipal(claimPrincipal);
                await _localStorage.SetItemAsync(AUTH_KEY, authResult.token);

                return user;
            }
            return null;
        }
        private ClaimsPrincipal CreateClaimsPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity();

            if (tokenHandler.CanReadToken(token))
            {
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
                identity = new(jwtSecurityToken.Claims, "jwt");
            }

            return new(identity);
        }
        public async Task<User?> FetchUserFromBrowser()
        {
            var token = await  _localStorage.GetItemAsync<string>(AUTH_KEY);
            var claimsPrincipal = CreateClaimsPrincipalFromToken(token);
            var user = User.FromClaimsPrincipal(claimsPrincipal);
           
            return user;
        }
        public void ClearBrowserUserData() => _localStorage.RemoveItemAsync(AUTH_KEY);
    }
}
