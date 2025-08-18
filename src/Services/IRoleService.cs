using src.DTOs;

namespace src.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<RoleDto?> GetRoleByIdAsync(int id);
        Task<RoleDto> CreateRoleAsync(CreateRoleDto roleDto);
        Task<bool> UpdateRoleAsync(int id, UpdateRoleDto roleDto);
        Task<bool> DeleteRoleAsync(int id);
    }
}
