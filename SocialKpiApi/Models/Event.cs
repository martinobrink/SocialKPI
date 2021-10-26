using System.ComponentModel.DataAnnotations;

namespace SocialKpiApi.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public EventCategory Category {  get; set; }
        public List<Employee>? Participants {  get; set; }
    }

    public enum EventCategory
    {
        Social,
        Health,
        Knowledge,
        Other
    }
}
