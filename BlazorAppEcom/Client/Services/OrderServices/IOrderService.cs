namespace BlazorAppEcom.Client.Services.OrderServices
{
    public interface IOrderService
    {
        Task PlaceOrder();
        Task<List<OrderOverviewResponseDTO>> GetOrders();
        Task<OrderDetailsResponseDTO> GetOrderDetailsProduct(int orderId);
    }
}
