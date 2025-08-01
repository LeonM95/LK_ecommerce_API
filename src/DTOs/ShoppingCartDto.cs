using test_LK_ecommerce.DTOs;

namespace src.DTOs
{
    public class ShoppingCartDto
    {
        public int CartId { get; set; }
        public DateTime? CartUpdatedDate { get; set; }

        // FK - Flattened data
        public string? UserName { get; set; }
        public string? StatusName { get; set; } 

        // List of items in the cart
        public List<CartProductDto> Items { get; set; } = new List<CartProductDto>();
    }
}
