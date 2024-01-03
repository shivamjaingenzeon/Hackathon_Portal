namespace hackathonbackend.Data
{
    public class Hackathon
    {
        public int HackathonId { get; set; }

        public string HackathonName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int NumberOfMembers { get; set; }
        public string Description { get; set; }
        // Other hackathon properties

        // Navigation properties
        public int? CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<UserStory> UserStories { get; set; }
        public ICollection<Team> Teams { get; set; }
    }

}
