using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class CreateAddressDto
    {
        [StringLength(200)]
        public string? AddressLine { get; set; }

        [StringLength(20)]
        public string? PostalCode { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        [StringLength(50)]
        public string? Country { get; set; }

        [Required]
        public int UserId { get; set; }

    }
}
