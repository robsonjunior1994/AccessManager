using System.ComponentModel.DataAnnotations;

namespace AccessManager.Api.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }
    }
}
