namespace SocialKpiApi.Models
{
    public class EventOutput
    {
        public string? Title { get; set; }
        public EventCategory Category { get; set; }
        public List<EmployeeOutput>? Participants { get; set; }
    }
}
