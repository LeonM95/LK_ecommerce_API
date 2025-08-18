using src.DTOs;

namespace src.Services
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto?> GetCartByUserIdAsync(int userId);
        Task<ShoppingCartDto> AddItemToCartAsync(int userId, AddItemToCartDto itemDto);
        Task<bool> UpdateItemQuantityAsync(int cartProductId, int quantity);
        Task<bool> RemoveItemFromCartAsync(int cartProductId);
    }
}
