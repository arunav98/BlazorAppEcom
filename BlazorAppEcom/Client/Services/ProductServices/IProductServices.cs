using Microsoft.AspNetCore.Components;

namespace BlazorAppEcom.Client.Services.ProductServices
{
    public interface IProductServices
    {
        event Action OnProductChange;
        List<Product> Products { get; set; }
        Task GetProducts(string? categoryUrl = null);

        Task<ServiceResponse<Product>> GetProduct(int productId);
    }
}
