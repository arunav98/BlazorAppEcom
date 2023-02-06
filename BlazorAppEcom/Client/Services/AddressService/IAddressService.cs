namespace BlazorAppEcom.Client.Services.AddressService
{
    public interface IAddressService
    {
        Task<Addresses> GetAddresses();
        Task<Addresses> AddOrUpdateAddresses(Addresses a);
    }
}
