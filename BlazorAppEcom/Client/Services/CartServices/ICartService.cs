namespace BlazorAppEcom.Client.Services.CartServices
{
    public interface ICartService
    {
        event Action onChange;
        Task AddToCart(CartItem cartItem);
        Task<List<CartItem>> GetAll();
        Task<List<CartProductResponseDTO>> GetCartProducts();
        Task RemoveCartItem(int productId,int productVariantId);
        Task UpdateQuantity(CartProductResponseDTO product);
        Task StoreCartItem(bool emptyLocalCart);
        Task GetItemCount();
    }
}
