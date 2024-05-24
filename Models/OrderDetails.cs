using PayPal.Api;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetApps.api.Models
{
    public class OrderDetails
    {
        public int Id { get; set; } 
        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        public Orders? Order { get; set; }
        public int PetId { get; set; }
        [ForeignKey("PetId")]
        public Pet? Pet { get; set; }
        public int Quantity { get; set; }
        public string unit { get; set; }
        public string payment { get; set; }
        public string Address { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhone { get; set; }

    }
}