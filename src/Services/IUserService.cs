using Microsoft.AspNetCore.Mvc;
using src.DTOs;

namespace src.Services
{
    public interface IUserService
    {

        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> AddUserAsync(CreateUserDto userDto);
        Task<bool> UpdateUserAsync(int id, UpdateUserDto userDto);
        Task<bool> DeleteUserAsync(int id);
    }
}
