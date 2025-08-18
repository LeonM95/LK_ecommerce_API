using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace src.Models.Entities
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }
        public required string UrlImage { get; set; }
        public required string Description { get; set; }
        public required int ProductId { get; set; }
        public required int? StatusId { get; set; }

        [ForeignKey("ProductId")]
        [JsonIgnore]
        public required Product Product { get; set; }

        [ForeignKey("StatusId")]
        [JsonIgnore]
        public required Status Status { get; set; }
    }
}
