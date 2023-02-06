namespace BlazorAppEcom.Server.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly DataContext _dataContext;
        private readonly IAuthService _authService;

        public AddressService(DataContext dataContext,IAuthService authService)
        {
            _dataContext = dataContext;
            _authService = authService;
        }
        public async Task<ServiceResponse<Addresses>> AddOrUpdateAddress(Addresses a)
        {
            var response = new ServiceResponse<Addresses>();
            var dbAddress = (await GetAddress()).Data;
            if (dbAddress == null)
            {
                a.UserId = _authService.GetUserId();
                _dataContext.Add(a);
                response.Data = a;
            }
            else
            {
                dbAddress.Name = a.Name;
                dbAddress.Street=a.Street;
                dbAddress.City=a.City;
                dbAddress.State=a.State;
                dbAddress.Pincode = a.Pincode;
                dbAddress.Country=a.Country;
                response.Data = dbAddress;
            }
            await _dataContext.SaveChangesAsync();
            return response;

        }

        public async Task<ServiceResponse<Addresses>> GetAddress()
        {
            int userId = _authService.GetUserId();
            var addresses = await _dataContext.Addresses.FirstOrDefaultAsync(a => a.UserId == userId);
            return new ServiceResponse<Addresses> { Data = addresses };
        }
    }
}
