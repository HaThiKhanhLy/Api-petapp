using System.ComponentModel.DataAnnotations.Schema;

namespace PetApps.api.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string NamePets { get; set; }
        
        public int PetsTypeID {  get; set; }
        [ForeignKey("PetsTypeID")]
        public PetTypes? PetTypes { get; set; }
        public string Gender { get; set; }
        public double Size { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public string Species {  get; set; }
        public string Image {  get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public int Unit {  get; set; }
    }
}
