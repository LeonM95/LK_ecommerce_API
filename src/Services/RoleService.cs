using AutoMapper;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.DTOs;
using src.Models.Entities;

namespace src.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        // to initialize the service with the database and mapper
        public RoleService(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // to get a list of all roles
        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _dbContext.Role.ToListAsync();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        // to get a single role by its id
        public async Task<RoleDto?> GetRoleByIdAsync(int id)
        {
            var role = await _dbContext.Role.FindAsync(id);
            return _mapper.Map<RoleDto>(role);
        }

        // to create a new role
        public async Task<RoleDto> CreateRoleAsync(CreateRoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            await _dbContext.Role.AddAsync(role);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<RoleDto>(role);
        }

        // to update a role
        public async Task<bool> UpdateRoleAsync(int id, UpdateRoleDto roleDto)
        {
            var role = await _dbContext.Role.FindAsync(id);
            if (role == null)
            {
                return false; // Indicates role was not found
            }
            _mapper.Map(roleDto, role);
            await _dbContext.SaveChangesAsync();
            return true; // Indicates success
        }

        // to delete a role
        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _dbContext.Role.FindAsync(id);
            if (role == null)
            {
                return false;
            }
            _dbContext.Role.Remove(role);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
