﻿namespace BlazorAppEcom.Server.Services.ProductService
{
    public class ProductServices : IProductServices
    {
        private readonly DataContext _context;

        public ProductServices(DataContext context)
        {
            _context = context;

        }
        public async Task<ServiceResponse<List<Product>>> GetProductAsync()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products.ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
            var response = new ServiceResponse<Product>();
            var product = await _context.Products.FindAsync(productId);
            if(product == null)
            {
                response.Success = false;
                response.Message = "No product with this product id";
            }
            else
            {
                response.Data = product;
            }
            return response;

        }
        public async Task<ServiceResponse<List<Product>>> GetProductByCategory(string CategoryUrl)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                .Where(p => p.Category.Url.ToLower().Equals(CategoryUrl.ToLower())).ToListAsync()
            };
            return response;
        }
    }
}
