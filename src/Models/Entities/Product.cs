using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace src.Models.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public required int Stock { get; set; }
        public required int CategoryId { get; set; }
        public required int StatusId { get; set; }
        public required int UserId { get; set; }

        [ForeignKey("CategoryId")]
        [JsonIgnore]
        public Category? Category { get; set; }

        [ForeignKey("StatusId")]
        [JsonIgnore]
        public Status? Status { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public Users? User { get; set; }

        public ICollection<Image> Images { get; set; } = new List<Image>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
