using System.ComponentModel.DataAnnotations;

namespace test_LK_ecommerce.DTOs
{
    public class UpdateCartProductDto
    {
        [Range(1, 100)]
        public int? Quantity { get; set; }
    }
}
