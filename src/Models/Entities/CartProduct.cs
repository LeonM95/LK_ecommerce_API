using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace src.Models.Entities
{
    public class CartProduct
    {
        [Key]
        public int CartProductsId { get; set; }

        public required int Quantity { get; set; }
        public DateTime? AddedDate { get; set; }

        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int StatusId { get; set; }

        public ShoppingCart? ShoppingCart { get; set; }

        [ForeignKey("ProductId")]
        [JsonIgnore]
        public Product? Product { get; set; }

        [ForeignKey("StatusId")]
        [JsonIgnore]
        public  Status? Status { get; set; }
    }
}
