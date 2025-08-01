using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class CreateReviewDto
    {
        [StringLength(300)]
        public string? ReviewText { get; set; }

        [Required]
        [Range(1, 5)] // range of stars/raiting
        public int Rating { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
