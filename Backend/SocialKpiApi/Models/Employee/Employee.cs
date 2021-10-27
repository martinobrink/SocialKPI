using System.ComponentModel.DataAnnotations;

namespace SocialKpiApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Initials { get; set; }
        public List<Event> Events { get; set; }
        public string? Phone { get; set; }
        public string? Title { get; set; }
        public string? Email { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Department { get; set; }
    }
}
