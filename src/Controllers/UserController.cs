using Microsoft.AspNetCore.Mvc;
using src.DTOs;
using src.Services;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase // use ControllerBase for APIs
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // to get list of all users as DTOs
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {

            var userDtos = await _userService.GetAllUsersAsync();
            return Ok(userDtos);
        }

        // to get a user by Id 
        [HttpGet("{id:int}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var userDtos = await _userService.GetUserByIdAsync(id);

            if(userDtos == null) {
                return NotFound();
            }

            return Ok(userDtos);
        }

        // to create a user from a DTO
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            var createdUserDto = await _userService.AddUserAsync(userDto);

            // to check if  email already exists
            if (createdUserDto == null)
            {
                return Conflict("A user with this email already exists.");
            }

            return CreatedAtAction(nameof(GetUserById), new { id = createdUserDto.UserId }, createdUserDto);
        }

        // to modify a user's profile data from a DTO
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto userDto)
        {
            var success = await _userService.UpdateUserAsync(id, userDto);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        // to delete a user
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}