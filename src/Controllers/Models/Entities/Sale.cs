using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text.Json.Serialization;

namespace src.Controllers.Models.Entities
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }
        public DateTime? SaleDate { get; set; }
        public required decimal Total { get; set; }
        public required int PaymentMethodId { get; set; }
        public required int StatusId { get; set; }
        public required int AddressId { get; set; }
        public required int UserId { get; set; } // Add this foreign key

        // Foreign Key Navigation Properties
        [ForeignKey("PaymentMethodId")]
        public required PaymentMethod PaymentMethod { get; set; }

        [ForeignKey("StatusId")]
        public required Status Status { get; set; }

        [ForeignKey("AddressId")]
        public required Address Address { get; set; }

        [ForeignKey("UserId")] 
        public Users? Users { get; set; } 

        public ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
    }
}
