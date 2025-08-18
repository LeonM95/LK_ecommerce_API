using src.DTOs;

namespace src.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDto>> GetAllAddressesByUserAsync(int userId);
        Task<AddressDto?> GetAddressByIdAsync(int addressId);
        Task<AddressDto> CreateAddressAsync(CreateAddressDto createAddressDto, int userId);
        Task<bool> UpdateAddressAsync(int addressId, UpdateAddressDto updateAddressDto);
        Task<bool> DeleteAddressAsync(int addressId);
    }
}
