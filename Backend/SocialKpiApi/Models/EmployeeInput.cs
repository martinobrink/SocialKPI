using System.ComponentModel.DataAnnotations;

namespace SocialKpiApi.Models
{
    public class EmployeeInput
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Initials { get; set; }
    }
}
