using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_LK_ecommerce.Data;
using test_LK_ecommerce.DTOs;
using test_LK_ecommerce.Controllers.Models.Entities;

namespace test_LK_ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase // use ControllerBase for APIs
    {
        private readonly ApplicationDBContext dBContext;
        private readonly IMapper _mapper; // to use AutoMapper

        // to initialize controller with DBContext and AutoMapper
        public CategoryController(ApplicationDBContext dBContext, IMapper mapper)
        {
            this.dBContext = dBContext;
            _mapper = mapper;
        }

        // to get list of all categories as DTOs
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            // to get the categories from the database
            var categories = await dBContext.Category
                .Include(c => c.Status) // include related Status data for mapping
                .ToListAsync();

            // to map the database entities to our public DTOs
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return Ok(categoryDtos);
        }

        // to get a single category by its id
        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await dBContext.Category
                .Include(c => c.Status) // also include Status here
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound(); // if not found, return a 404 error
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        // to create a new category from a DTO
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {
            // to map the incoming DTO to our internal Category entity
            var category = _mapper.Map<Category>(categoryDto);

            dBContext.Category.Add(category);
            await dBContext.SaveChangesAsync();

            // to map the new entity back to a DTO to return to the client
            var createdDto = _mapper.Map<CategoryDto>(category);

            // return a 201 Created status with a link to the new category
            return CreatedAtRoute("GetCategoryById", new { id = category.CategoryId }, createdDto);
        }

        // to update a category from a DTO
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto categoryDto)
        {
            var category = await dBContext.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            // use automapper to update the entity from the dto in one line
            _mapper.Map(categoryDto, category);

            await dBContext.SaveChangesAsync();

            // return 204 No Content, the standard for a successful update
            return NoContent();
        }

        // to delete a category by its id
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await dBContext.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            dBContext.Category.Remove(category);
            await dBContext.SaveChangesAsync();

            // return 204 No Content for a successful delete
            return NoContent();
        }
    }
}