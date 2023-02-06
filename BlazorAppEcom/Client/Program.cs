global using BlazorAppEcom.Shared;
global using System.Net.Http.Json;
global using BlazorAppEcom.Client.Services.ProductServices;
global using BlazorAppEcom.Client.Services.CategoryServices;
global using BlazorAppEcom.Client.Services.OrderServices;
global using BlazorAppEcom.Client.Services.CartServices;
global using Microsoft.AspNetCore.Components.Authorization;
global using BlazorAppEcom.Client.Services.AuthService;
global using BlazorAppEcom.Client.Services.AddressService;
using BlazorAppEcom.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();   
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ICartService, CartServices>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
