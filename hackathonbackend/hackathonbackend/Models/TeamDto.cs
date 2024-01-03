using hackathonbackend.Data;

namespace hackathonbackend.Models
{
    public class TeamDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int NumberOfMembers { get; set; }
        public string College { get; set; }

        public string Contact { get; set; }
       
        public int? HackathonId { get; set; }

               public int? UserStoryId { get; set; }

       
    }
}
