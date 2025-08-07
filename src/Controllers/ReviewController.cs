using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.DTOs;
using test_LK_ecommerce.Controllers.Models.Entities;
using test_LK_ecommerce.Data;
using test_LK_ecommerce.DTOs;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDBContext dBContext;
        private readonly IMapper _mapper;

        // to initialize controller
        public ReviewController(ApplicationDBContext dBContext, IMapper mapper)
        {
            this.dBContext = dBContext;
            _mapper = mapper;
        }


        // to get a list of all reviews of a product ID
        [HttpGet("product/{productId:int}")]
        public async Task<IActionResult> GetAllReviewsOfProduct(int productId)
        {
            var review = await dBContext.Review
                .Include(r => r.User)
                .Include(r => r.Product)
                .Where(r => r.ProductId == productId)
                .ToListAsync();

            var reviewDto = _mapper.Map<IEnumerable<ReviewDto>>(review);
            return Ok(reviewDto);
        }

        // to get a list of all reviews of a product ID
        [HttpGet("review/{reviewId:int}")]
        public async Task<IActionResult> GetReviewById(int reviewId)
        {
            var review = await dBContext.Review
                .Include(r => r.User)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(r => r.ReviewId == reviewId);

            // to check if the review exists
            if (review == null)
            {
                return NotFound(); // Return 404 if not found
            }

            var reviewDto = _mapper.Map<ReviewDto>(review);
            return Ok(reviewDto);
        }


        // to create a review
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);

            // to set the review date on the server
            review.ReviewDate = DateTime.UtcNow;
            review.StatusId = 1; // Active

            await dBContext.Review.AddAsync(review);
            await dBContext.SaveChangesAsync();

            var createReviewDto  = _mapper.Map<ReviewDto>(review);

            return CreatedAtAction(nameof(GetReviewById), new { reviewId = review.ReviewId },
                createReviewDto);
        }

        // to update a Review from a DTO
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] UpdateReviewDto reviewDto)
        {
            var review = await dBContext.Review.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            _mapper.Map(reviewDto, review);
            await dBContext.SaveChangesAsync();

            return NoContent();
        }


        // to mark review as removed 
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await dBContext.Review.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            review.StatusId = 3; // 3 for  "Deleted" status

            await dBContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
