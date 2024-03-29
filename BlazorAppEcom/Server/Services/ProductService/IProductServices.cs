﻿namespace BlazorAppEcom.Server.Services.ProductService
{
    public interface IProductServices
    {
        Task<ServiceResponse<List<Product>>> GetProductAsync();
        Task<ServiceResponse<Product>> GetProductAsync(int productId);
        Task<ServiceResponse<List<Product>>> GetProductByCategory(string CategoryUrl);
        Task<ServiceResponse<ProductSearchResultDTO>> SearchProducts(string query,int page);
        Task<ServiceResponse<List<String>>> SearchSuggestion(string query);
    }
}
