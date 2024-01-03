namespace hackathonbackend.Data
{
    public class UserStory
    {
        public int UserStoryId { get; set; }
        public string UserStoryName { get; set; }
        public string FrontendTechnology { get; set; }
        public string BackendTechnology { get; set; }
        public string Database { get; set; }
        public string Description { get; set; }

        public bool IsAssigned { get; set; }
        // Other user story properties

        // Navigation properties
        public int? HackathonId { get; set; }
        public Hackathon Hackathon { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
}
