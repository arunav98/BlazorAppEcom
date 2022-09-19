using BlazorAppEcom.Shared;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace BlazorAppEcom.Server.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;

        }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }


        public async Task<ServiceResponse<List<Category>>> GetCategories()
        {
            var response = new ServiceResponse<List<Category>>
            {
                Data = await _context.Categories.ToListAsync()
            };
            return response;
        }

        
    }
}
