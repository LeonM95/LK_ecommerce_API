using AutoMapper;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.DTOs;
using src.Models.Entities;

namespace src.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        // to initialize the service with the database and mapper
        public ProductService(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // to get a list of all products by user ID
        public async Task<IEnumerable<ProductDto>> GetProductsByUserAsync(int userId)
        {
            var products = await _dbContext.Product
                .Include(p => p.Category)
                .Include(p => p.Status)
                .Where(p => p.UserId == userId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        // to get a single product by its id
        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _dbContext.Product
                .Include(p => p.Category)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return null;
            }
            return _mapper.Map<ProductDto>(product);
        }

        // to create a new product from a DTO
        public async Task<ProductDto> CreateProductAsync(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            product.StatusId = 1; // Default to "Active"

            await _dbContext.Product.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        // to update a product from a DTO
        public async Task<bool> UpdateProductAsync(int id, UpdateProductDto productDto)
        {
            var product = await _dbContext.Product.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _mapper.Map(productDto, product);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        // to "soft delete" a product by changing its status
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _dbContext.Product.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            product.StatusId = 3; // Assuming '3' is "Deleted"
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}