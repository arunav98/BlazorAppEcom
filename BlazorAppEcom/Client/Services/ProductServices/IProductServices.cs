using Microsoft.AspNetCore.Components;

namespace BlazorAppEcom.Client.Services.ProductServices
{
    public interface IProductServices
    {
        event Action OnProductChange;
        List<Product> Products { get; set; }
        Task GetProducts(string? categoryUrl = null);

        Task<ServiceResponse<Product>> GetProduct(int productId);
        string Message { get; set; }
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        string LastQuery { get; set; }  

        Task SearchProducts(string? query = null, int page=1);
        Task<List<string>> SearchSuggestion(string query);
    }
}
