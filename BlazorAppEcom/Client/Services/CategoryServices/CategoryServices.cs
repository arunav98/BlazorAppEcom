using BlazorAppEcom.Shared;
using static System.Net.WebRequestMethods;

namespace BlazorAppEcom.Client.Services.CategoryServices
{
    public class CategoryServices : ICategoryServices
    {
        private HttpClient _http;

        public CategoryServices(HttpClient http)
        {
            _http = http;
        }

        public List<Category> Categories { get; set; } = new List<Category>();

        public async Task GetCategories()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/category");
            if (result != null && result.Data != null)
                Categories = result.Data;
        }
    }
}
