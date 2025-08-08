using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.DTOs;
using src.Utils;
using src.Controllers.Models.Entities;
using src.Data;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase // use ControllerBase for APIs
    {
        private readonly ApplicationDBContext dBContext;
        private readonly IMapper _mapper; // to use AutoMapper

        // to initialize controller
        public UserController(ApplicationDBContext dBContext, IMapper mapper)
        {
            this.dBContext = dBContext;
            _mapper = mapper;
        }

        // to get list of all users as DTOs
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await dBContext.Users
                .Include(u => u.Role)   // include Role for mapping RoleName
                .Include(u => u.Status) // include Status for mapping StatusName
                .ToListAsync();

            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(userDtos);
        }

        // to get a user by Id 
        [HttpGet("{id:int}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await dBContext.Users
                .Include(u => u.Role)
                .Include(u => u.Status)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        // to create a user from a DTO
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            var user = _mapper.Map<Users>(userDto);

            // to hash the password before saving (never save plain text)

            user.Password = PasswordHasher.Hash(userDto.Password!);

            // to set default values on the server for security
            user.RoleId = 2; // Default to "Vendor" or "Buyer"
            user.StatusId = 1; // Default to "Active"

            await dBContext.Users.AddAsync(user);
            await dBContext.SaveChangesAsync();

            var createdUserDto = _mapper.Map<UserDto>(user);

            return CreatedAtRoute("GetUserById", new { id = user.UserId }, createdUserDto);
        }

        // to modify a user's profile data from a DTO
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto userDto)
        {
            var user = await dBContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // to update only the fields allowed by the DTO
            _mapper.Map(userDto, user);
            await dBContext.SaveChangesAsync();

            return NoContent();
        }

        // to delete a user
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await dBContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            dBContext.Users.Remove(user);
            await dBContext.SaveChangesAsync();

            return NoContent();
        }
    }
}