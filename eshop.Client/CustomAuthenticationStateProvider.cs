using eshop.Client.Models;
using eshop.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace eshop.Client
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
    {
        private readonly UserService _userService;
        public User CurrentUser { get; private set; } = new();
        public CustomAuthenticationStateProvider(UserService userService)
        {
            _userService = userService;
            AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var isValid = await _userService.ValidateJwtTokenAsync();
            var principal = new ClaimsPrincipal(new ClaimsIdentity());
            if (isValid)
            {             
                var user = await _userService.FetchUserFromBrowser();
                CurrentUser = user;
                principal = user.ToClaimsPrincipal();
                
            }
            return new(principal);
        }

        public async Task LoginAsync(string username, string password)
        {
            var principal = new ClaimsPrincipal();
            var user = await _userService.SendAuthenticateRequestAsync(username, password);
            CurrentUser = user;
            
            if (user is not null)
            {
                var isValid = await _userService.ValidateJwtTokenAsync();
                if(isValid)
                    principal = user.ToClaimsPrincipal();
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }
        public void Logout()
        {
            _userService.ClearBrowserUserData();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
        }
        private async void OnAuthenticationStateChangedAsync(Task<AuthenticationState> task)
        {
            var authenticationState = await task;

            if (authenticationState is not null)
            {
                CurrentUser = User.FromClaimsPrincipal(authenticationState.User);
            }
        }
        public void Dispose() => AuthenticationStateChanged -= OnAuthenticationStateChangedAsync;
    }
}
