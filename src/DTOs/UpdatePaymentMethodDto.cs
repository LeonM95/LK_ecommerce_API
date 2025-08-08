using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class UpdatePaymentMethodDto
    {
        [Required]
        public string? PaymentMethodName { get; set; }
    }
}
