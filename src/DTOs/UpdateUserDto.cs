using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class UpdateUserDto
    {

        [StringLength(100)]
        public string? Fullname { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Description { get; set; }
    }
}
