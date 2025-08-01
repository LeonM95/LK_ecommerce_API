using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using test_LK_ecommerce.Controllers.Models.Entities;

namespace test_LK_ecommerce.DTOs
{
    public class ImageDto
    {
        public int ImageId { get; set; }
        public required string UrlImage { get; set; }
        public string? Description { get; set; }
        public int ProductId { get; set; }
        public required string StatusName { get; set; }

    }
}
