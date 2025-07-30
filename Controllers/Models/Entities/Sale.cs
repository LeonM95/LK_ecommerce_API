using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text.Json.Serialization;

namespace test_LK_ecommerce.Controllers.Models.Entities
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }
        public DateTime? SaleDate { get; set; }
        public required decimal Total { get; set; }
        public required int PaymentMethodId { get; set; }
        public required int StatusId { get; set; }
        public required int ProductId { get; set; }
        public required int AddressId { get; set; }

        [ForeignKey("PaymentMethodId")]
        [JsonIgnore]
        public required PaymentMethod PaymentMethod { get; set; }

        [ForeignKey("StatusId")]
        [JsonIgnore]
        public required Status Status { get; set; }

        [ForeignKey("ProductId")]
        [JsonIgnore]
        public required Product Product { get; set; }

        [ForeignKey("AddressId")]
        [JsonIgnore]
        public required Address Address { get; set; }
    }
}
