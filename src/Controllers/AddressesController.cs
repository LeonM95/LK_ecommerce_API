using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Controllers.Models.Entities;
using src.Data;
using src.DTOs;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {

        private readonly ApplicationDBContext dBContext;
        private readonly IMapper _mapper;

        // to initialize controller
        public AddressesController(ApplicationDBContext dBContext, IMapper mapper)
        {
            this.dBContext = dBContext;
            _mapper = mapper;
        }


        // to get a list of all addresses by user ID
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetAllAddressesByUser(int userId)
        {
            var address = await dBContext.Address
                .Include(a => a.Users) 
                .Include(a => a.Status) 
                .Where(a => a.UserId == userId)
                .Where(p => p.UserId == userId)
                .ToListAsync();

            var addressDto = _mapper.Map<IEnumerable<AddressDto>>(address);
            return Ok(addressDto);
        }


        // to get one address by its Id
        [HttpGet("{addressId:int}", Name = "GetAddressById")]
        public async Task<IActionResult> GetAddressById(int addressId)
        {
            var address = await dBContext.Address
                .Include(a => a.Users)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(sc => sc.AddressId == addressId);

            // to check if the address was found before mapping
            if (address == null)
            {
                return NotFound(); // Return 404 if not found
            }

            var addressDto = _mapper.Map<AddressDto>(address);
            return Ok(addressDto);
        }


        // to create a new address from a DTO
        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressDto addressDto)
        {
            var address = _mapper.Map<Address>(addressDto);

            // to set default status
            address.StatusId = 1; // Active

            await dBContext.Address.AddAsync(address);
            await dBContext.SaveChangesAsync();

            var createAddressDto = _mapper.Map<AddressDto>(address);

            return CreatedAtAction(nameof(GetAddressById), new { addressId = address.AddressId }, createAddressDto);
        }


        // to update a product from a DTO
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] UpdateAddressDto addressDto)
        {
            var address = await dBContext.Address.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            _mapper.Map(addressDto, address);
            await dBContext.SaveChangesAsync();

            return NoContent();
        }


        // to mark product as inactive 
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await dBContext.Address.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            address.StatusId = 3; // 3 for  "Deleted" status

            await dBContext.SaveChangesAsync();

            return NoContent();
        }


    }
}
