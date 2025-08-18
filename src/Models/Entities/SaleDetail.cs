using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace src.Models.Entities
{
    public class SaleDetail
    {
        [Key]
        public int SalesDetailsId { get; set; }

        public required int Quantity { get; set; }
        public required decimal UnitPrice { get; set; }
        public required decimal Subtotal { get; set; }

        public int SaleId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("SaleId")]
        [JsonIgnore]
        public Sale? Sale { get; set; }

        [ForeignKey("ProductId")]
        [JsonIgnore]
        public Product? Product { get; set; }
    }
}