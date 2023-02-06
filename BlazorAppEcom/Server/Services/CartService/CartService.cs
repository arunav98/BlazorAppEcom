using BlazorAppEcom.Shared;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorAppEcom.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private DataContext _context;
        private readonly IAuthService _authService;

        public CartService(DataContext context,IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<int>> GetCardItemCount()
        {
            var count = (await _context.CartItems.Where(ci => ci.UserId == _authService.GetUserId()).ToListAsync()).Count();
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
            cartItems.ForEach(ci => ci.UserId = _authService.GetUserId());
            _context.CartItems.AddRange(cartItems);
            await _context.SaveChangesAsync();

            return await GetDbCartProduct();

        }

        public async Task<ServiceResponse<List<CartProductResponseDTO>>> GetDbCartProduct()
        {
            return await GetCartProduct(
                await _context.CartItems.Where(ci => ci.UserId == _authService.GetUserId()).ToListAsync()
);
        }

        public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
        {
            cartItem.UserId = _authService.GetUserId();

            var sameItem = await _context.CartItems.FirstOrDefaultAsync(ci=>
            ci.ProductId==cartItem.ProductId && ci.ProductTypeId==cartItem.ProductTypeId 
            && ci.UserId==_authService.GetUserId());

            if (sameItem == null)
            {
                _context.CartItems.Add(cartItem);
            }
            else
            {
                sameItem.Quantity = cartItem.Quantity;
            }

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> UpdateToCart(CartItem cartItem)
        {
            var sameItem = await _context.CartItems.FirstOrDefaultAsync(ci =>
            ci.ProductId == cartItem.ProductId && ci.ProductTypeId == cartItem.ProductTypeId
            && ci.UserId == cartItem.UserId);

            if (sameItem == null)
            {
                return new ServiceResponse<bool> 
                { Data = false,Success=false,Message="Cart Item Doesn't Exist" };
            }
                sameItem.Quantity = cartItem.Quantity;
            

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> RemoveItem(int productId, int productTypeId)
        {
            var item = await _context.CartItems.FirstOrDefaultAsync(ci =>
            ci.ProductId == productId && ci.ProductTypeId == productTypeId
            && ci.UserId == _authService.GetUserId());

            if (item == null)
            {
                return new ServiceResponse<bool>
                { Data = false, Success = false, Message = "Cart Item Doesn't Exist" };
            }
            


            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };

        }
    }
}
