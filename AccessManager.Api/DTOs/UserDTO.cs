using System.ComponentModel.DataAnnotations;

namespace AccessManager.Api.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        public CompanyDTO CompanyDTO { get; set; }
    }
}
