using src.DTOs;

namespace src.Services
{
    public interface IStatusService
    {
        Task<IEnumerable<StatusDto>> GetAllStatusesAsync();
        Task<StatusDto?> GetStatusByIdAsync(int id);
        Task<StatusDto> CreateStatusAsync(CreateStatusDto statusDto);
        Task<bool> UpdateStatusAsync(int id, UpdateStatusDto statusDto);
        Task<bool> DeleteStatusAsync(int id);
    }
}
