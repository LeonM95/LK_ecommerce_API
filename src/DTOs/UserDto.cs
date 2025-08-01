namespace src.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; } 
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; } 

        // FK - Flattened data 
        public string? RoleName { get; set; }
        public string? StatusName { get; set; } 
    }
}
