using Microsoft.AspNetCore.Mvc;
using src.DTOs;
using src.Services;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        // to initialize the controller with the product service
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // to get a list of all products by user ID
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetAllProductsByUser(int userId)
        {
            var productDtos = await _productService.GetProductsByUserAsync(userId);
            return Ok(productDtos);
        }

        // to get a single product by its id
        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var productDto = await _productService.GetProductByIdAsync(id);
            if (productDto == null)
            {
                return NotFound();
            }
            return Ok(productDto);
        }

        // to create a new product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            var createdProductDto = await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProductDto.ProductId }, createdProductDto);
        }

        // to update a product
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto productDto)
        {
            var success = await _productService.UpdateProductAsync(id, productDto);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        // to delete a product
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _productService.DeleteProductAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}