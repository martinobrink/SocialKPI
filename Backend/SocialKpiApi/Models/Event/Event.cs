using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SocialKpiApi.Models;

namespace SocialKpiApi.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public EventCategory Category {  get; set; }
        public List<Employee>? Participants { get; set; }
        public DateTimeOffset TimeOfEvent { get; set; }
        public string CreatedBy { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EventCategory
    {
        Social,
        Health,
        Knowledge,
        Other
    }
}
