using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class CreateCategoryDto
    {
        [Required]
        [StringLength(100)]
        public string? CategoryName { get; set; }

        [StringLength(50)]
        public string? Reference { get; set; }

        [Required]
        public int StatusId { get; set; }
    }
}
