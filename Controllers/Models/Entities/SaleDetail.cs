using System.ComponentModel.DataAnnotations;

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

        public required Sale Sale { get; set; }
        public required Product Product { get; set; }
    }
}
