using BlazorAppEcom.Shared;
using System.Net.Http.Json;

namespace BlazorAppEcom.Client.Services.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly HttpClient _http;

        public ProductServices(HttpClient http)
        {
            _http = http;
        }

        public List<Product> Products { get; set; } = new List<Product>();
        public Product OneProduct { get; set; } = new Product();
        public string Message { get; set; } = "Loading Product...";

        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string LastQuery { get; set; } = string.Empty;

        public event Action OnProductChange;

        public async Task<ServiceResponse<Product>> GetProduct(int productId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}");    
            return result;
        }

        public async Task GetProducts(string? categoryUrl = null)
        {
            
            var result = categoryUrl == null? await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product")
                        : await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");
            if (result != null && result.Data != null)
                Products = result.Data;
            if (Products.Count == 0) Message = "No Product Found";

            OnProductChange.Invoke();

        }

        public async Task SearchProducts(string query,int page)
        {
            LastQuery = query;  
            var result = await _http.GetFromJsonAsync<ServiceResponse<ProductSearchResultDTO>>($"api/product/search/{query}/{page}");
            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            if (Products.Count == 0) Message = "No Product Found";
   
            OnProductChange?.Invoke();
        }

        public async Task<List<string>> SearchSuggestion(string query)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchSuggestion/{query}");
            return  result.Data;

        }
    }
}
