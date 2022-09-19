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

            OnProductChange.Invoke();

        }
    }
}
