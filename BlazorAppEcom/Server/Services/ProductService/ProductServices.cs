using BlazorAppEcom.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppEcom.Server.Services.ProductService
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
                Data = await _context.Products.Include(p=>p.Variants).ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
            var response = new ServiceResponse<Product>();
            var product = await _context.Products
                .Include(p => p.Variants)
                .ThenInclude(p => p.ProductType)
                .FirstOrDefaultAsync(p=> p.Id==productId);

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
                Data = await _context.Products.Include(p => p.Variants)
                .Where(p => p.Category.Url.ToLower().Equals(CategoryUrl.ToLower())).ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<ProductSearchResultDTO>> SearchProducts(string query, int page)
        {
            var pageSize = 2f;
            var pageCount = Math.Ceiling((await FindProductSearch(query)).Count /pageSize);
            var products = await _context.Products
                                .Where(p => p.Title.ToLower().Contains(query.ToLower())
                                || p.Description.ToLower().Contains(query.ToLower()))
                                .Include(p => p.Variants)
                                .Skip((int)((page - 1) * pageSize))
                                .Take((int)pageSize)
                                .ToListAsync();
            var response = new ServiceResponse<ProductSearchResultDTO>
            {
                Data = new ProductSearchResultDTO
                {
                    Products = products,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };
            return response;
        }

        private async Task<List<Product>> FindProductSearch(string query)
        {
            return await _context.Products
                                .Where(p => p.Title.ToLower().Contains(query.ToLower())
                                || p.Description.ToLower().Contains(query.ToLower()))
                                .Include(p => p.Variants).ToListAsync();
        }

        public async Task<ServiceResponse<List<string>>> SearchSuggestion(string query)
        {
            var products = await FindProductSearch(query);

            List<string> result = new List<string>();
            foreach (var item in products)
            {
                if (item.Title.ToLower().Contains(query.ToLower())){
                    result.Add(item.Title);
                }
                if (item.Description != null)
                {
                    var panctuation = item.Description.Where(char.IsPunctuation).Distinct().ToArray();
                    var words = item.Description.Split().Select(p => p.Trim(panctuation)).Distinct();
                    foreach (var word in words)
                    {
                        if (word.ToLower().Contains(query.ToLower()))
                        {
                            result.Add(word);
                        }
                    }
                }
            }
            result = result.Distinct().ToList();
            Console.WriteLine(result.Distinct().OrderBy(x => x).ToList());
            return new ServiceResponse<List<string>> { Data = result };
        }
    }
}
