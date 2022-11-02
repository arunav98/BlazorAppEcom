using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorAppEcom.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(DataContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<int>> GetCardItemCount()
        {
            var count = (await _context.CartItems.Where(ci => ci.UserId == GetUserId()).ToListAsync()).Count();
            return new ServiceResponse<int> { Data=count};
        }

        public async Task<ServiceResponse<List<CartProductResponseDTO>>> GetCartProduct(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartProductResponseDTO>>
            {
                Data=new List<CartProductResponseDTO>()
            };

            foreach (var cartItem in cartItems)
            {
                var product = await _context.Products
                    .Where(p => p.Id == cartItem.ProductId)
                    .FirstOrDefaultAsync();
                if(product != null)
                {
                    var productVariant = await _context.ProductVariants
                                        .Where(v => v.ProductId == cartItem.ProductId &&
                                        v.ProductTypeId == cartItem.ProductTypeId)
                                        .Include(v=>v.ProductType)
                                        .FirstOrDefaultAsync();
                    if (productVariant != null)
                    {
                        var cartProduct = new CartProductResponseDTO
                        {
                            ProductId = cartItem.ProductId,
                            Title = product.Title,
                            ImageUrl = product.ImageUrl,
                            Price = productVariant.Price,
                            ProductType = productVariant.ProductType.Name,
                            ProductTypeId = productVariant.ProductTypeId,
                            Quantity=cartItem.Quantity,
                        };
                        result.Data.Add(cartProduct);
                    }
                }
            }
            return result;
            
        }

        public async Task<ServiceResponse<List<CartProductResponseDTO>>> StoreCartItems(List<CartItem> cartItems)
        {
            cartItems.ForEach(ci => ci.UserId = GetUserId());
            _context.CartItems.AddRange(cartItems);
            await _context.SaveChangesAsync();

            return await GetDbCartProduct();

        }

        public async Task<ServiceResponse<List<CartProductResponseDTO>>> GetDbCartProduct()
        {
            return await GetCartProduct(
                await _context.CartItems.Where(ci => ci.UserId == GetUserId()).ToListAsync()
                );
        }
    }
}
