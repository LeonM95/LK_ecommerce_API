using System.ComponentModel.DataAnnotations;

namespace test_LK_ecommerce.DTOs
{
    public class CreateCartProductDto
    {
        [Required]
        public int Quantity { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

    }
}
