using System.ComponentModel.DataAnnotations.Schema;

namespace PetApps.api.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Status {  get; set; }
        public double Total { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
