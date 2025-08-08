using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace src.Controllers.Models.Entities
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string? ReviewText { get; set; } 
        public DateTime? ReviewDate { get; set; }
        public int Raiting {  get; set; }

        public int UserId { get; set; }
        public required int ProductId { get; set; }

        public required int StatusId { get; set; }

        [ForeignKey("ProductId")]
        [JsonIgnore]
        public required Product Product { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public required Users User { get; set; }

        [ForeignKey("StatusId")] 
        [JsonIgnore]
        public Status? Status { get; set; } 
    }
}
