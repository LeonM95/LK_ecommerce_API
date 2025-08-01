using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class UpdateReviewDto
    {
        [StringLength(300)]
        public string? ReviewText { get; set; }

        [Range(1, 5)] // range of stars/raiting
        public int? Rating { get; set; }
    }
}
