using System.ComponentModel.DataAnnotations;

namespace SocialKpiApi.Models
{
    public class EmployeeInput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Initials { get; set; }
    }
}
