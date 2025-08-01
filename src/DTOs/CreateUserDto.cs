using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class CreateUserDto
    {
        [Required]
        [StringLength(100)]
        public string? Fullname { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string? Password { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        public string? Description { get; set; }
    }
}
