using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using src.Controllers.Models.Entities;

namespace src.DTOs
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
