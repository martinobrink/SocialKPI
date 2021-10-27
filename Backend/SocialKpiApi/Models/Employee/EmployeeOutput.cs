namespace SocialKpiApi.Models
{
    public class EmployeeOutput
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string? Phone { get; set; }
        public string? Title { get; set; }
        public string? Email { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Department { get; set; }
    }
}
