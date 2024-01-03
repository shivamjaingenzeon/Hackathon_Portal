namespace hackathonbackend.Data
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Location { get; set; }
        // Other company properties

        // Navigation properties
        public int? AuthenticationUserId { get; set; }
        public AuthenticationUser AuthenticationUser { get; set; }
        public ICollection<Team> Teams { get; set; }
        public ICollection<Hackathon> Hackathons { get; set; }
    }
}
