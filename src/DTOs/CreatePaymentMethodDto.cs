using System.ComponentModel.DataAnnotations;

namespace test_LK_ecommerce.DTOs
{
    public class CreatePaymentMethodDto
    {
        [Required]
        public string? PaymentMethodName { get; set; }
    }
}
