using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace src.Models.Entities
{
    public class ShoppingCart
    {
        [Key]
        public int CartId { get; set; }
        public DateTime? CartAddedDate { get; set; }
        public DateTime? CartUpdatedDate { get; set; }
        public int UserId { get; set; }
        public  int StatusId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public  Users? User { get; set; }

        [ForeignKey("StatusId")]
        [JsonIgnore]
        public  Status? Status { get; set; }

        // collection to represent the items in the cart
        public ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();
    }
}
