using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class CreateSaleDetailDto
    {
        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int SaleId { get; set; }
    }
}
