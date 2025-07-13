using System.ComponentModel.DataAnnotations;

namespace test_LK_ecommerce.Controllers.Models.Entities
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }
        public required string UrlImage { get; set; }
        public required string Description { get; set; }
        public required int ProductId { get; set; }
        public required int? StatusId { get; set; }

        public required Product Product { get; set; }
        public Status? Status { get; set; }
    }
}
