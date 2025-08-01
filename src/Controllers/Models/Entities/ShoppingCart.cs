using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace test_LK_ecommerce.Controllers.Models.Entities
{
    public class ShoppingCart
    {
        [Key]
        public int CartId { get; set; }
        public DateTime? CartAddedDate { get; set; }
        public required DateTime? CartUpdatedDate { get; set; }
        public required int UserId { get; set; }
        public required int StatusId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public required Users Users { get; set; }

        [ForeignKey("StatusId")]
        [JsonIgnore]
        public required Status Status { get; set; }
    }
}
