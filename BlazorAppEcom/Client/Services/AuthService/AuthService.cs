using BlazorAppEcom.Shared;
using System.Net.Http.Json;

namespace BlazorAppEcom.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private HttpClient _httpClient;
        private AuthenticationStateProvider _authenticationStateProvider;

        public AuthService(HttpClient httpClient,AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/change-password", request.Password);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<bool> IsUserAuthenticated()
        {
            return (await _authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> Register(UserRegistration request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/register",request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
    }
}
