using System.ComponentModel.DataAnnotations;

namespace test_LK_ecommerce.DTOs
{
    public class CreateImageDto
    {
        [Required]
        [Url] // to use URL attribute for URL validation
        public  string? UrlImage { get; set; }
        public  string? Description { get; set; }
        [Required]
        public int ProductId { get; set; }

    }
}
