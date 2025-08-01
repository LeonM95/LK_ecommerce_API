using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class CreateSaleDto
    {
        [Required]
        public int PaymentMethodId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int AddressId { get; set; }
    }
}
