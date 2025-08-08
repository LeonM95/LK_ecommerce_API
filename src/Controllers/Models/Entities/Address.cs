using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace src.Controllers.Models.Entities
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public required string AddressLine { get; set; }
        public required string PostalCode { get; set; }    
        public required string City { get; set; }
        public required string Country { get; set; }
        public required int UserId { get; set; }
        public required int StatusId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public required Users Users { get; set; }
        [ForeignKey("StatusId")]
        [JsonIgnore]
        public required Status Status { get; set; }
    }
}
