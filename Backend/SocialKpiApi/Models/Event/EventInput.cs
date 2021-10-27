using SocialKpiApi.Models;

namespace SocialKpiApi.Models
{
    public class EventInput
    {
        public string? Title { get; set; }
        public EventCategory Category { get; set; }
        public List<EmployeeInput>? Participants { get; set; }
        public DateTimeOffset TimeOfEvent { get; set; }
        public string CreatedBy { get; set; }
    }
}
