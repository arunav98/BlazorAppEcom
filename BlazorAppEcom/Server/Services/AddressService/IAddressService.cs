﻿namespace BlazorAppEcom.Server.Services.AddressService
{
    public interface IAddressService
    {
        Task<ServiceResponse<Addresses>> GetAddress();
        Task<ServiceResponse<Addresses>> AddOrUpdateAddress(Addresses a);
    }
}
