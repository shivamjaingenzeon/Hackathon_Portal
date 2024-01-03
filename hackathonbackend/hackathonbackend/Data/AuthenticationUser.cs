using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace hackathonbackend.Data
{
    public class AuthenticationUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }
         
        public string PasswordHash { get; set; }
        public bool IsCompany { get; set; } 
    }
}
