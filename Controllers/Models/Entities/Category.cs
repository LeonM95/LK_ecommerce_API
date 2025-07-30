using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace test_LK_ecommerce.Controllers.Models.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public required string Reference { get; set; }
        public required int StatusId { get; set; }

        [ForeignKey("StatusId")]
        [JsonIgnore]
        public Status? Status { get; set; }
    }
}
