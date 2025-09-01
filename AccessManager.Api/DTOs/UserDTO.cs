using AccessManager.Api.Models;
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

        public UserDTO()
        {
            CompanyDTO = new CompanyDTO();
        }

        public UserDTO(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            CompanyDTO = new CompanyDTO(user.Company);
        }
    }
}
