using Blazored.LocalStorage;
using Blazored.Modal;
using eshop.Client;
using eshop.Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredModal();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5270/api") });
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
sp.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddScoped<BasketService>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
