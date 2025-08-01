using System.ComponentModel.DataAnnotations;

namespace test_LK_ecommerce.Controllers.Models.Entities
{
    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodId { get; set; }
        public required string MethodName { get; set; }
    }
}
