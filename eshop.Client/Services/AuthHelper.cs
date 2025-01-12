using Microsoft.AspNetCore.Components.Authorization;

namespace eshop.Client.Services
{
    public static class AuthHelper
    {
        public static async Task<bool> IsAuth(AuthenticationStateProvider _authStateProvider)
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            return authState is not null && authState.User.Identity.IsAuthenticated;
        }
    }
}
