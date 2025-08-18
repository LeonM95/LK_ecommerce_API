using AutoMapper;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.DTOs;
using src.Utils;
using src.Models.Entities;

namespace src.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        // to initialize the service with the database and mapper
        public UserService(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // to get a list of all users
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _dbContext.Users
                .Include(u => u.Role)
                .Include(u => u.Status)
                .ToListAsync();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        // to get a single user
        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _dbContext.Users
                .Include(u => u.Role)
                .Include(u => u.Status)
                .FirstOrDefaultAsync(u => u.UserId == id);

            return _mapper.Map<UserDto>(user);
        }

        // to create a new user
        public async Task<UserDto> AddUserAsync(CreateUserDto userDto)
        {
            // to check if the email is already in use
            if (await _dbContext.Users.AnyAsync(u => u.Email == userDto.Email))
            {
                return null; 
            }

            var user = _mapper.Map<Users>(userDto);

            // to hash the password 
            user.Password = PasswordHasher.Hash(userDto.Password!);

            // to set default values 
            user.RoleId = 3; // Default to 'Buyer'
            user.StatusId = 1; // Default to 'Active'

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        // to update a user profile
        public async Task<bool> UpdateUserAsync(int id, UpdateUserDto userDto)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return false; 
            }

            _mapper.Map(userDto, user);
            await _dbContext.SaveChangesAsync();
            return true; // Indicates success
        }

        // to soft delete a user
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            user.StatusId = 3; // 3 for "Deleted"
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
