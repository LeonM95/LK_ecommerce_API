using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.DTOs;
using src.Models.Entities;
using src.Services;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        // to initialize the controller with the review service
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // to get a list of all reviews for a product
        [HttpGet("product/{productId:int}")]
        public async Task<IActionResult> GetAllReviewsForProduct(int productId)
        {
            var reviews = await _reviewService.GetAllReviewsForProductAsync(productId);
            return Ok(reviews);
        }

        // to get a review by its Id
        [HttpGet("{reviewId:int}", Name = "GetReviewById")]
        public async Task<IActionResult> GetReviewById(int reviewId)
        {
            var review = await _reviewService.GetReviewByIdAsync(reviewId);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        // to create a review
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto reviewDto)
        {
            var newReview = await _reviewService.CreateReviewAsync(reviewDto);
            return CreatedAtAction(nameof(GetReviewById), new { reviewId = newReview.ReviewId }, newReview);
        }

        // to update a review
        [HttpPatch("{reviewId:int}")]
        public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] UpdateReviewDto reviewDto)
        {
            var success = await _reviewService.UpdateReviewAsync(reviewId, reviewDto);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        // to delete a review
        [HttpDelete("{reviewId:int}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var success = await _reviewService.DeleteReviewAsync(reviewId);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
