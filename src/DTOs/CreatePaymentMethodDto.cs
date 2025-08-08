using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class CreatePaymentMethodDto
    {
        [Required]
        public string? PaymentMethodName { get; set; }
    }
}
