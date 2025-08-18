using src.DTOs;

namespace src.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetAllReviewsForProductAsync(int productId);
        Task<ReviewDto?> GetReviewByIdAsync(int reviewId);
        Task<ReviewDto> CreateReviewAsync(CreateReviewDto reviewDto);
        Task<bool> UpdateReviewAsync(int reviewId, UpdateReviewDto reviewDto);
        Task<bool> DeleteReviewAsync(int reviewId);
    }
}
