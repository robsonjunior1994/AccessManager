using AccessManager.Api.DTOs;

namespace AccessManager.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; }
        public Company Company { get; set; }
    }
}
