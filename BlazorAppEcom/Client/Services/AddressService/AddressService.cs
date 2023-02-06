using BlazorAppEcom.Shared;
using System.Net.Http.Json;

namespace BlazorAppEcom.Client.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly HttpClient _httpClient;

        public AddressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Addresses> AddOrUpdateAddresses(Addresses a)
        {
            var response = await _httpClient.PostAsJsonAsync("api/address", a);
            return response.Content.ReadFromJsonAsync<ServiceResponse<Addresses>>().Result.Data;
        }

        public async Task<Addresses> GetAddresses()
        {
            var response = await _httpClient.GetFromJsonAsync < ServiceResponse<Addresses>>("api/address");
            return response.Data;
        }
    }
}
