using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_LK_ecommerce.Controllers.Models.Entities;
using test_LK_ecommerce.Data;

namespace test_LK_ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext dBContext;

        // to initialize controller
        public CategoryController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        // to get list of all categories
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var category = await dBContext.Category.ToListAsync();
            return Ok(category);
        }


        // to create a category
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            category.Status = null;

            dBContext.Category.Add(category);
            await dBContext.SaveChangesAsync();

            return Ok(category);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category updateCategory)
        {


            var category = await dBContext.Category.FindAsync(id);
            if (category == null)
                return NotFound();

            category.CategoryName = updateCategory.CategoryName;
            category.Reference = updateCategory.Reference;
            category.Status = updateCategory.Status;


            await dBContext.SaveChangesAsync();

            return Ok(category);
        }
    }
}
