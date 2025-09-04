namespace AccessManager.Api.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public List<User> Users { get; set; }
    }
}
