using System.ComponentModel.DataAnnotations;

namespace src.Models.Entities
{
    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodId { get; set; }
        public required string MethodName { get; set; }
    }
}
