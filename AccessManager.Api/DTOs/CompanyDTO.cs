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

        public CompanyDTO() { }
        public CompanyDTO(Models.Company company)
        {
            Id = company.Id;
            Name = company.Name;
            Number = company.Number;
        }
    }
}
