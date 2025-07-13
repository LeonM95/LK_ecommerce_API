using System.ComponentModel.DataAnnotations;
using System.Net;

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

        public required PaymentMethod PaymentMethod { get; set; }
        public required Status Status { get; set; }
        public required Product Product { get; set; }
        public required Address Address { get; set; }
    }
}
