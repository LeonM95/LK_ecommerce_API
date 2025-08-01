using System.ComponentModel.DataAnnotations;

namespace test_LK_ecommerce.DTOs
{
    public class UpdateProductDto
    {
        [StringLength(100)]
        public string? ProductName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0.01, 10000.00)]
        public decimal? Price { get; set; }

        [Range(0, int.MaxValue)]
        public int? Stock { get; set; }

        public int? CategoryId { get; set; }

        public int? StatusId { get; set; }
    }
}
