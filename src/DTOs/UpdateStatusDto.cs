using System.ComponentModel.DataAnnotations;

namespace src.DTOs
{
    public class UpdateStatusDto
    {
        [StringLength(50)]
        public string? StatusDescription{ get; set; }
    }
}
