using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace src.Controllers.Models.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public required string RoleName { get; set; }

    }
}
