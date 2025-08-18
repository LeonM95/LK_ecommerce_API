using AutoMapper;
using Microsoft.EntityFrameworkCore;
using src.Models.Entities;
using src.Data;
using src.DTOs;

namespace src.Services
{
    public class ImageService : IImageService
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        // to initialize the service
        public ImageService(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // to get all images for a specific product
        public async Task<IEnumerable<ImageDto>> GetAllImagesForProductAsync(int productId)
        {
            var images = await _dbContext.Image
                .Include(i => i.Status)
                .Where(i => i.ProductId == productId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ImageDto>>(images);
        }

        // to get a single image by its id
        public async Task<ImageDto?> GetImageByIdAsync(int imageId)
        {
            var image = await _dbContext.Image
                .Include(i => i.Status)
                .FirstOrDefaultAsync(i => i.ImageId == imageId);
            return _mapper.Map<ImageDto>(image);
        }

        // to create a new image
        public async Task<ImageDto> CreateImageAsync(CreateImageDto imageDto)
        {
            var image = _mapper.Map<Image>(imageDto);
            image.StatusId = 1; // Default to "Active"

            await _dbContext.Image.AddAsync(image);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ImageDto>(image);
        }

        // to update an image
        public async Task<bool> UpdateImageAsync(int imageId, UpdateImageDto imageDto)
        {
            var image = await _dbContext.Image.FindAsync(imageId);
            if (image == null)
            {
                return false;
            }
            _mapper.Map(imageDto, image);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        // to soft delete an image
        public async Task<bool> DeleteImageAsync(int imageId)
        {
            var image = await _dbContext.Image.FindAsync(imageId);
            if (image == null)
            {
                return false;
            }
            image.StatusId = 3; // Assuming '3' is "Deleted"
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
