global using BlazorAppEcom.Shared;
global using System.Net.Http.Json;
global using BlazorAppEcom.Client.Services.ProductServices;
global using BlazorAppEcom.Client.Services.CategoryServices;
using BlazorAppEcom.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
