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
        public List<Event> Events {  get; set; }
    }
}
