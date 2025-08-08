using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace src.Controllers.Models.Entities
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

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
