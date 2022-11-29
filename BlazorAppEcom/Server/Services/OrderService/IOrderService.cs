namespace BlazorAppEcom.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<bool>> PalceOrder();
    }
}
