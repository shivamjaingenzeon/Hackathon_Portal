namespace hackathonbackend.Models
{
    public class HackathonDto
    {
        public string HackathonName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int NumberOfMembers { get; set; }
        public string Description { get; set; }
    }
}
