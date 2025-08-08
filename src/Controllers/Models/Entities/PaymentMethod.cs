using System.ComponentModel.DataAnnotations;

namespace src.Controllers.Models.Entities
{
    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodId { get; set; }
        public required string MethodName { get; set; }
    }
}
