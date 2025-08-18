using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.DTOs;
using src.Models.Entities;
using src.Services;
using System.Security.Claims;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {

        private readonly IAddressService _addressService;

        // to initialize the controller with the address service
        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // to get a list of all addresses by user ID
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetAllAddressesByUser(int userId)
        {
            var addressDtos = await _addressService.GetAllAddressesByUserAsync(userId);
            return Ok(addressDtos);
        }

        // to get one address by its Id
        [HttpGet("{addressId:int}", Name = "GetAddressById")]
        public async Task<IActionResult> GetAddressById(int addressId)
        {
            var addressDto = await _addressService.GetAddressByIdAsync(addressId);

            if (addressDto == null)
            {
                return NotFound();
            }

            return Ok(addressDto);
        }

        // to create a new address from a DTO
        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressDto createAddressDto)
        {
            // Find the user Id from ttoken
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);


            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }

            // Parse the string ID to an integer
            if (!int.TryParse(userIdString, out var userId))
            {
                return BadRequest("Invalid user ID format in token.");
            }

            var testUserId = 1;
            var createdAddressDto = await _addressService.CreateAddressAsync(createAddressDto, testUserId);

            return CreatedAtAction(nameof(GetAddressById), new { addressId = createdAddressDto.AddressId }, createdAddressDto);
        }


        // to update an address from a DTO
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] UpdateAddressDto addressDto)
        {
            var success = await _addressService.UpdateAddressAsync(id, addressDto);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // to mark an address as deleted
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var success = await _addressService.DeleteAddressAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
