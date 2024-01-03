using hackathonbackend.Data;

namespace hackathonbackend.Models
{
    public class UserStoryDto
    {
        public string UserStoryName { get; set; }
        public string FrontendTechnology { get; set; }
        public string BackendTechnology { get; set; }
        public string Database { get; set; }
        public string Description { get; set; }

        public bool IsAssigned { get; set; }
        // Other user story properties

        // Navigation properties
        public int HackathonId { get; set; }
        public int TeamId { get; set; }
    }
}
