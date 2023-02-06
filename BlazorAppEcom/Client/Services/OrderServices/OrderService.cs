using BlazorAppEcom.Client.Services.AuthService;
using Microsoft.AspNetCore.Components;

namespace BlazorAppEcom.Client.Services.OrderServices
{
    public class OrderService : IOrderService 
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        private readonly IAuthService _authService;
        private readonly IAddressService _addressService;

        public OrderService(HttpClient http,
            NavigationManager navigationManager,IAuthService authService,IAddressService addressService)
        {
            _http = http;
            _navigationManager = navigationManager;
            _authService = authService;
            _addressService = addressService;
        }

        public async Task<OrderDetailsResponseDTO> GetOrderDetailsProduct(int orderId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<OrderDetailsResponseDTO>>($"api/order/{orderId}");
            return result.Data;
        }

        public async Task<List<OrderOverviewResponseDTO>> GetOrders()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<OrderOverviewResponseDTO>>>("api/order");
            return result.Data;
        }

        public async Task PlaceOrder()
        {
            if(await _authService.IsUserAuthenticated())
            {
                if(_addressService.GetAddresses()!=null)
                    await _http.PostAsync("api/order", null);
                else
                    _navigationManager.NavigateTo("profile");
            }
            else
            {
                _navigationManager.NavigateTo("login");
            }
        }
    }
}
