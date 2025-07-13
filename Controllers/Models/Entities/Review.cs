using System.ComponentModel.DataAnnotations;

namespace test_LK_ecommerce.Controllers.Models.Entities
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string? ReviewText { get; set; }  
        public required int ProductId { get; set; }
        public required Product Product { get; set; }
    }
}
