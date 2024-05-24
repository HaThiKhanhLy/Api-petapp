

using System.ComponentModel.DataAnnotations.Schema;

namespace PetApps.api.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        public Orders? Order { get; set; }

        public string? PaymentCode { get; set; }
        public DateTime Date { get; set; }
        public string? Method { get; set; }
    }
}
