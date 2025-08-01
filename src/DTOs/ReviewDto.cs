using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using test_LK_ecommerce.Controllers.Models.Entities;

namespace test_LK_ecommerce.DTOs
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public string? ReviewText { get; set; }
        public DateTime? ReviewDate { get; set; }
        public int Rating { get; set; }

        // related FK entities for easy display
        public string?  ProductName { get; set; }
        public string?  UserName { get; set; }

    }
}
