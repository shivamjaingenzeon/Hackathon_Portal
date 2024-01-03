namespace hackathonbackend.Data
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfMembers { get; set; }
        public string College { get; set; }
        
        public string Contact { get; set; }
        // Other team properties

        // Navigation properties
        public int? AuthenticationUserId { get; set; }
        public AuthenticationUser AuthenticationUser { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public int? HackathonId { get; set; }

        public Hackathon Hackathon { get; set; }

        public int? UserStoryId { get; set; }

        public UserStory UserStory { get; set; }

        public ICollection<Member> Members { get; set; }
    }
}
