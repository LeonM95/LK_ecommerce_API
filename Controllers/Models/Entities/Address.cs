using System.ComponentModel.DataAnnotations;

namespace test_LK_ecommerce.Controllers.Models.Entities
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

        public required Users Users { get; set; }
        public required Status Status { get; set; }
    }
}
