namespace SocialKpiApi.Models
{
    public class EventOutput
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public EventCategory Category { get; set; }
        public List<EmployeeOutput>? Participants { get; set; }
        public DateTimeOffset TimeOfEvent { get; set; }
        public string CreatedBy { get; set; }
    }
}
