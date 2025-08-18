using src.DTOs;

namespace src.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsByUserAsync(int userId);
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(CreateProductDto productDto);
        Task<bool> UpdateProductAsync(int id, UpdateProductDto productDto);
        Task<bool> DeleteProductAsync(int id);
    }
}
