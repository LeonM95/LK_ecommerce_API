using System.ComponentModel.DataAnnotations;

namespace test_LK_ecommerce.DTOs
{
    public class UpdateImageDto
    {
        [Url] // Use the [Url] attribute for URL validation
        public string? UrlImage { get; set; }
        public string? Description { get; set; }
        public int? StatudId { get; set; }
    }
}
