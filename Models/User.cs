using System.ComponentModel.DataAnnotations;

namespace PetApps.api.Models
{
    public class User
    {
        public int ID { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Address { get; set; }
        public DateTime? DayOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Role { get; set; }
        public int? Status { get; set; }

    }
}
