using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Controllers.Models.Entities; 
using src.Data;                       
using src.DTOs;                       
using src.Utils;

namespace src.Controllers
{
    public class ImagesController : ControllerBase
    {
        private readonly ApplicationDBContext dBContext;
        private readonly IMapper _mapper;

        // to initialize controller
        public ImagesController(ApplicationDBContext dBContext, IMapper mapper)
        {
            this.dBContext = dBContext;
            _mapper = mapper;
        }

        // to get a list of all images bt product
        [HttpGet("product/{productId:int}")]
        public async Task<IActionResult> GetAllImagesOfProduct(int productId)
        {
            var images = await dBContext.Image
                .Include(i => i.Product)
                .Include(i => i.Status)
                .Where(i => i.ProductId == productId)
                .ToListAsync();

            var imagesDto = _mapper.Map<IEnumerable<ImageDto>>(images);
            return Ok(imagesDto);
        }

        // to get an image by Id
        [HttpGet("{imageId:int}", Name = "GetImageById")]
        public async Task<IActionResult> GetImageById(int imageId)
        {
            var images = await dBContext.Image
                .Include(i => i.Product)
                .Include(i => i.Status)
                .FirstOrDefaultAsync(i => i.ImageId == imageId);

            if (images == null)
            {
                return NotFound();
            }

            var imagesDto = _mapper.Map<ImageDto>(images);
            return Ok(imagesDto);
        }


        // to create a image 
        [HttpPost]
        public async Task<IActionResult> CreateImage([FromBody] CreateImageDto imageDto)
        {
            var image = _mapper.Map<Image>(imageDto);

            // to set default status
            image.StatusId = 1; // Active

            await dBContext.Image.AddAsync(image);
            await dBContext.SaveChangesAsync();

            var createImageDto = _mapper.Map<ImageDto>(image);

            return CreatedAtAction(nameof(GetImageById), new { imageId = image.ImageId }, createImageDto);
        }



        // to update a image from a DTO
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateImage(int id, [FromBody] UpdateImageDto imageDto)
        {
            var image = await dBContext.Image.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            _mapper.Map(imageDto, image);
            await dBContext.SaveChangesAsync();

            return NoContent();
        }


        // to mark a image as inactive 
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await dBContext.Image.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            image.StatusId = 3; // 3 for  "Deleted" status

            await dBContext.SaveChangesAsync();

            return NoContent();
        }


    }
}
