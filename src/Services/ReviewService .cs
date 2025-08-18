using AutoMapper;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.DTOs;
using src.Models.Entities;

namespace src.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        // to initialize the service
        public ReviewService(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // to get all reviews for a specific product
        public async Task<IEnumerable<ReviewDto>> GetAllReviewsForProductAsync(int productId)
        {
            var reviews = await _dbContext.Review
                .Include(r => r.User)
                .Include(r => r.Product)
                .Where(r => r.ProductId == productId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        // to get a single review by its id
        public async Task<ReviewDto?> GetReviewByIdAsync(int reviewId)
        {
            var review = await _dbContext.Review
                .Include(r => r.User)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(r => r.ReviewId == reviewId);
            return _mapper.Map<ReviewDto>(review);
        }

        // to create a new review
        public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);
            review.ReviewDate = DateTime.UtcNow;
            review.StatusId = 1; // Default to "Active"

            await _dbContext.Review.AddAsync(review);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ReviewDto>(review);
        }

        // to update a review
        public async Task<bool> UpdateReviewAsync(int reviewId, UpdateReviewDto reviewDto)
        {
            var review = await _dbContext.Review.FindAsync(reviewId);
            if (review == null)
            {
                return false;
            }
            _mapper.Map(reviewDto, review);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        // to soft delete a review
        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            var review = await _dbContext.Review.FindAsync(reviewId);
            if (review == null)
            {
                return false;
            }
            review.StatusId = 3; // 3 for "Deleted"
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
