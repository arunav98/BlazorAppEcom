namespace BlazorAppEcom.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartProductResponseDTO>>> GetCartProduct(List<CartItem> cartItems);
        Task<ServiceResponse<List<CartProductResponseDTO>>> StoreCartItems(List<CartItem> cartItems);
        Task<ServiceResponse<int>> GetCardItemCount();
        Task<ServiceResponse<List<CartProductResponseDTO>>> GetDbCartProduct();
    }
}
