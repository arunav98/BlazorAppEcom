using BlazorAppEcom.Server.Services.CartService;
using BlazorAppEcom.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorAppEcom.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("products")]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponseDTO>>>> GetCartProduct(List<CartItem> cartItems)
        {
            var result = await _cartService.GetCartProduct(cartItems);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponseDTO>>>> StoreCartItem(List<CartItem> cartItems)
        {
          
            var result = await _cartService.StoreCartItems(cartItems);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddCartItem(CartItem cartItems)
        {

            var result = await _cartService.AddToCart(cartItems);
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateCartItem(CartItem cartItems)
        {

            var result = await _cartService.UpdateToCart(cartItems);
            return Ok(result);
        }

        [HttpGet("count")]
        public async Task<ActionResult<ServiceResponse<int>>> CartItemCount()
        {
            var result = await _cartService.GetCardItemCount();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponseDTO>>>> GetDbCartProduct()
        {

            var result = await _cartService.GetDbCartProduct();
            return Ok(result);
        }
    }
}
