using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.DTOs;
using src.Models.Entities;
using src.Services;
using src.Utils;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;

        // to initialize the controller with the image service
        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        // to get a list of all images for a product
        [HttpGet("product/{productId:int}")]
        public async Task<IActionResult> GetAllImagesForProduct(int productId)
        {
            var images = await _imageService.GetAllImagesForProductAsync(productId);
            return Ok(images);
        }

        // to get an image by its Id
        [HttpGet("{imageId:int}", Name = "GetImageById")]
        public async Task<IActionResult> GetImageById(int imageId)
        {
            var image = await _imageService.GetImageByIdAsync(imageId);
            if (image == null)
            {
                return NotFound();
            }
            return Ok(image);
        }

        // to create an image
        [HttpPost]
        public async Task<IActionResult> CreateImage([FromBody] CreateImageDto imageDto)
        {
            var newImage = await _imageService.CreateImageAsync(imageDto);
            return CreatedAtAction(nameof(GetImageById), new { imageId = newImage.ImageId }, newImage);
        }

        // to update an image
        [HttpPatch("{imageId:int}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromBody] UpdateImageDto imageDto)
        {
            var success = await _imageService.UpdateImageAsync(imageId, imageDto);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        // to delete an image
        [HttpDelete("{imageId:int}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var success = await _imageService.DeleteImageAsync(imageId);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
