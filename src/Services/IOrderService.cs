using src.DTOs;

namespace src.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<SaleDto>> GetOrdersForUserAsync(int userId);
        Task<SaleDto?> GetOrderByIdAsync(int orderId);
        Task<SaleDto?> CreateOrderFromCartAsync(int userId, CreateSaleDto orderDto);
    }
}
