namespace hackathonbackend.Models
{
    public class MemberDto
    {
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public char Gender { get; set; }
        public string CollegeName { get; set; }
        public long ContactNumber { get; set; }
        public string Email { get; set; }
        public string Skill { get; set; }

        public long AadharNumber { get; set; }
        public string Role { get; set; }
        public string Contact { get; set; }
        // Other member properties

        // Navigation properties
        public int TeamId { get; set; }
    }
}
