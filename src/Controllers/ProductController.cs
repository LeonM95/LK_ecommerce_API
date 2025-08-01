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

        // to get a list of all products by user ID
        [HttpGet]
        public async Task<IActionResult> GetAllProductsByUser(int userId)
        {
            var products = await dBContext.Product
                .Include(p => p.Category) // include related data for mapping
                .Include(p => p.Status).// include related data for mapping
                 Where(p => p.UserId == userId)
                .ToListAsync();

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }


        // to search a list of all that container a search term
        [HttpGet("search")]
        public async Task<IActionResult> SearchProductsByName([FromQuery] string name)
        {
            // to validate that the search term is not empty
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Search term is empty.");
            }

            var searchTerm = name.ToLower(); 

            var products = await dBContext.Product
                .Include(p => p.Category)
                .Include(p => p.Status)
                .Where(p => p.ProductName.ToLower().Contains(searchTerm)) // the search logic
                .ToListAsync();

            // it's standard to return an empty list if no results are found
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }

        // to get a single product by its id
        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await dBContext.Product
                .Include(p => p.Category)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        // to create a new product from a DTO
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            await dBContext.Product.AddAsync(product);
            await dBContext.SaveChangesAsync();

            var createdProductDto = _mapper.Map<ProductDto>(product);

            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, createdProductDto);
        }

        // to update a product from a DTO
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto productDto)
        {
            var product = await dBContext.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _mapper.Map(productDto, product);
            await dBContext.SaveChangesAsync();

            return NoContent(); // return 204 No Content for a successful update
        }

        // to delete a product
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await dBContext.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            dBContext.Product.Remove(product);
            await dBContext.SaveChangesAsync();

            return NoContent(); // return 204 No Content for a successful delete
        }
    }
}