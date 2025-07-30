using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace test_LK_ecommerce.Controllers.Models.Entities
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string? ReviewText { get; set; }  
        public required int ProductId { get; set; }


        [ForeignKey("ProductId")]
        [JsonIgnore]
        public required Product Product { get; set; }
    }
}
