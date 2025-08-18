namespace src.Models.Entities
{
    public class Status
    {
        public int StatusId { get; set; }
        public required string StatusDescription { get; set; }

        public ICollection<Users> Users { get; set; } = new List<Users>();
    }
}
