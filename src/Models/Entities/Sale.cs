using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace src.Models.Entities
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
        public required int UserId { get; set; }

        [ForeignKey("PaymentMethodId")]
        public PaymentMethod? PaymentMethod { get; set; }

        [ForeignKey("StatusId")]
        public Status? Status { get; set; }

        [ForeignKey("AddressId")]
        public Address? Address { get; set; }

        [ForeignKey("UserId")]
        public Users? User { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
    }
}