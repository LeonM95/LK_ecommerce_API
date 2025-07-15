using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    }
}
