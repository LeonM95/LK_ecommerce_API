using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class AddItemToCartDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
