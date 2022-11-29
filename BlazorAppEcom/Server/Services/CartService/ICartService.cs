namespace BlazorAppEcom.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartProductResponseDTO>>> GetCartProduct(List<CartItem> cartItems);
        Task<ServiceResponse<List<CartProductResponseDTO>>> StoreCartItems(List<CartItem> cartItems);
        Task<ServiceResponse<int>> GetCardItemCount();
        Task<ServiceResponse<List<CartProductResponseDTO>>> GetDbCartProduct();
        Task<ServiceResponse<bool>> AddToCart(CartItem cartItem);
        Task<ServiceResponse<bool>> UpdateToCart(CartItem cartItem);
        Task<ServiceResponse<bool>> RemoveItem(int productId, int productTypeId);
    }
}
