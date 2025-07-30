using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_LK_ecommerce.Data;
using test_LK_ecommerce.API.DTOs;
using test_LK_ecommerce.Controllers.Models.Entities; // TO use DTOs

namespace test_LK_ecommerce.API.DTOs // Specified use of DTOs
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDBContext dBContext;
        private readonly IMapper _mapper; 

        // to initialize controller
        public ProductController(ApplicationDBContext dBContext, IMapper mapper)
        {
            this.dBContext = dBContext;
            _mapper = mapper;
        }

        // to get list of all products by userId
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllProductsByUser(int userId)
        {
            var products = await dBContext.Product.
                Where(p => p.UserId == userId).ToListAsync();

            if (products == null || !products.Any()) { 
                return NotFound("There are not products for this user");
            }
            //using mapper DTO
            var productDtos = _mapper.Map<IEnumerable<ProductController>>(products);

            return Ok(productDtos);
        }


        // to create a product
        //[HttpPost]
        //public async Task<IActionResult> CreateProduct([FromBody] Product product)
        //{
        //    // to avoid creating a new role and user
        //    if (user.Role != null)
        //        dBContext.Entry(user.Role).State = EntityState.Unchanged;
        //    if (user.Status != null)
        //        dBContext.Entry(user.Status).State = EntityState.Unchanged;

        //    dBContext.Users.Add(user);
        //    await dBContext.SaveChangesAsync();

        //    return Ok(user);
        //}

    }
}
