using System.ComponentModel.DataAnnotations.Schema;

namespace PetApps.api.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int PetID { get; set; }
        [ForeignKey("PetID")]
        public Pet? Pet { get; set; }
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User? User { get; set; }
    }
}