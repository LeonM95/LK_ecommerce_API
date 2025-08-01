using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace test_LK_ecommerce.Controllers.Models.Entities
{
    public class SaleDetail
    {
        [Key]
        public int SalesDetailsId { get; set; }
        public required int Quantity { get; set; }
        public required decimal UnitPrice { get; set; }
        public required decimal Subtotal { get; set; }
        public required int SaleId { get; set; }
        public required int ProductId { get; set; }

        [ForeignKey("SaleId")]
        [JsonIgnore]
        public required Sale Sale { get; set; }

        [ForeignKey("ProductId")]
        [JsonIgnore]
        public required Product Product { get; set; }
    }
}
