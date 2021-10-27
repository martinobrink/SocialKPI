using System.ComponentModel.DataAnnotations;

namespace SocialKpiApi.Models
{
    public class EmployeeInput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Initials { get; set; }
        public string? Phone { get; set; }
        public string? Title { get; set; }
        public string? Email { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Department { get; set; }
    }
}
