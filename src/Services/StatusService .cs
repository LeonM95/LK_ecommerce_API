using AutoMapper;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.DTOs;
using src.Models.Entities;

namespace src.Services
{
    public class StatusService : IStatusService
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        // to initialize the service with the database and mapper
        public StatusService(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // to get a list of all statuses
        public async Task<IEnumerable<StatusDto>> GetAllStatusesAsync()
        {
            var statuses = await _dbContext.Status.ToListAsync();
            return _mapper.Map<IEnumerable<StatusDto>>(statuses);
        }

        // to get a single status by its id
        public async Task<StatusDto?> GetStatusByIdAsync(int id)
        {
            var status = await _dbContext.Status.FindAsync(id);
            return _mapper.Map<StatusDto>(status);
        }

        // to create a new status
        public async Task<StatusDto> CreateStatusAsync(CreateStatusDto statusDto)
        {
            var status = _mapper.Map<Status>(statusDto);
            await _dbContext.Status.AddAsync(status);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<StatusDto>(status);
        }

        // to update a status
        public async Task<bool> UpdateStatusAsync(int id, UpdateStatusDto statusDto)
        {
            var status = await _dbContext.Status.FindAsync(id);
            if (status == null)
            {
                return false; // Indicates status was not found
            }
            _mapper.Map(statusDto, status);
            await _dbContext.SaveChangesAsync();
            return true; // Indicates success
        }

        // to delete a status
        public async Task<bool> DeleteStatusAsync(int id)
        {
            var status = await _dbContext.Status.FindAsync(id);
            if (status == null)
            {
                return false;
            }
            _dbContext.Status.Remove(status);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
