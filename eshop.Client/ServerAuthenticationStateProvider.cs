using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
namespace eshop.Client
{
    public class ServerAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;

        public ServerAuthenticationStateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var handler = new JwtSecurityTokenHandler();
            var getToken = await _httpClient.GetFromJsonAsync<string>("api/me");
            var token = handler.ReadJwtToken(getToken);
            ClaimsPrincipal user;
            if (token is null)
            {
                user = new ClaimsPrincipal(new ClaimsIdentity());
            }
            else
            {
                
                user = new ClaimsPrincipal(new ClaimsIdentity(token.Claims, "jwt"));
            }

            return new AuthenticationState(user);
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            await _httpClient.PostAsJsonAsync("api/authenticate", new { Token = token });
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _httpClient.PostAsync("api/logout", null);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
