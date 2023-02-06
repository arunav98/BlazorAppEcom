using BlazorAppEcom.Server.Services.AddressService;
using BlazorAppEcom.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppEcom.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<Addresses>>> GetAddress()
        {
            return await _addressService.GetAddress();
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Addresses>>> AddOrUpdateAddress(Addresses addresses)
        {
            return await _addressService.AddOrUpdateAddress(addresses);
        }
    }
}
