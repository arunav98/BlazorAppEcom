namespace BlazorAppEcom.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<bool>> PalceOrder();
        Task<ServiceResponse<List<OrderOverviewResponseDTO>>> GetOrderOverview();
        Task<ServiceResponse<OrderDetailsResponseDTO>> GetOrderDetails(int orderId);
    }
}
