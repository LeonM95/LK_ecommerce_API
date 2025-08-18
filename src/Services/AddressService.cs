using AutoMapper;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.DTOs;
using src.Models.Entities;

namespace src.Services
{
    public class AddressService : IAddressService
    {

        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        // to initialize the service with the database and mapper
        public AddressService(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AddressDto>> GetAllAddressesByUserAsync(int userId)
        {
            var addresses = await _dbContext.Address
                .Include(a => a.Users)
                .Include(a => a.Status)
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AddressDto>>(addresses);
        }


        public async Task<AddressDto?> GetAddressByIdAsync(int addressId)
        {
            var address = await _dbContext.Address
                .Include(a => a.Users)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(sc => sc.AddressId == addressId);

            if (address == null)
            {
                return null;
            }

            return _mapper.Map<AddressDto>(address);
        }

        // create a new Address
        // ...
        public async Task<AddressDto> CreateAddressAsync(CreateAddressDto createAddressDto, int userId)
        {
            var address = _mapper.Map<Address>(createAddressDto);

            // Set the UserId from the authenticated user, not the DTO
            address.UserId = userId;

            // Set default status
            address.StatusId = 1; // Active

            await _dbContext.Address.AddAsync(address);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<AddressDto>(address);
        }
        // ...

        // update address of user
        public async Task<bool> UpdateAddressAsync(int addressId, UpdateAddressDto updateAddressDto)
        {
            var address = await _dbContext.Address.FindAsync(addressId);

            if (address == null)
            {
                return false;
            }

            _mapper.Map(updateAddressDto, address);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        // soft delete
        public async Task<bool> DeleteAddressAsync(int addressId)
        {
            var address = await _dbContext.Address.FindAsync(addressId);

            if (address == null)
            {
                return false;
            }

            address.StatusId = 3; // "Deleted" status

            await _dbContext.SaveChangesAsync();

            return true;
        }

    }
}
