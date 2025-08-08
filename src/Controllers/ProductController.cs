using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.DTOs;
using src.Controllers.Models.Entities;

namespace src.Controllers
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
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetAllProductsByUser(int userId)
        {
            var products = await dBContext.Product
                .Include(p => p.Category)
                .Include(p => p.Status)
                .Where(p => p.UserId == userId)
                .ToListAsync();

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }

        // to search a list of all that contain a search term
        [HttpGet("search")]
        public async Task<IActionResult> SearchProductsByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Search term is empty.");
            }

            var searchTerm = name.ToLower();

            var products = await dBContext.Product
                .Include(p => p.Category)
                .Include(p => p.Status)
                .Where(p => p.ProductName.ToLower().Contains(searchTerm))
                .ToListAsync();

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

            // to set default status
            product.StatusId = 1; // Active

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

            return NoContent();
        }

        // to mark product as inactive 
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await dBContext.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.StatusId = 3; // 3 for  "Deleted" status

            await dBContext.SaveChangesAsync();

            return NoContent();
        }


  
    }
}