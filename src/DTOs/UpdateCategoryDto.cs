using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class UpdateCategoryDto
    {
        [StringLength(100)]
        public string? CategoryName { get; set; }

        [StringLength(50)]
        public string? Reference { get; set; }

        public int? StatusId { get; set; }
    }
}
