using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace test_LK_ecommerce.Controllers.Models.Entities
{
    public class CartProduct
    {
        [Key]
        public int CartProductsId { get; set; }

        public required int Quantity { get; set; }
        public DateTime? AddedDate { get; set; }

        public required int CartId { get; set; }
        public required int ProductId { get; set; }
        public required int StatusId { get; set; }

        public required ShoppingCart ShoppingCart { get; set; }

        [ForeignKey("ProductId")]
        [JsonIgnore]
        public required Product Product { get; set; }

        [ForeignKey("StatusId")]
        [JsonIgnore]
        public required Status Status { get; set; }
    }
}
