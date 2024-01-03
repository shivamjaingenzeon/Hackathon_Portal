namespace hackathonbackend.Models
{
    public class TeamLoginResponseDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int NumberOfMembers { get; set; }
        public string College { get; set; }
        public string Contact { get; set; }
        public string Token { get; set; }
    }

}
