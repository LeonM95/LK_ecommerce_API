using src.DTOs;

namespace src.Services
{
    public interface IImageService
    {
        Task<IEnumerable<ImageDto>> GetAllImagesForProductAsync(int productId);
        Task<ImageDto?> GetImageByIdAsync(int imageId);
        Task<ImageDto> CreateImageAsync(CreateImageDto imageDto);
        Task<bool> UpdateImageAsync(int imageId, UpdateImageDto imageDto);
        Task<bool> DeleteImageAsync(int imageId);
    }
}
