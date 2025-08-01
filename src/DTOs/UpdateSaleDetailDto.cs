using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class UpdateSaleDetailDto
    {
        [Required]
        [Range(1, 100)]
        public int? Quantity { get; set; }
    }
}
