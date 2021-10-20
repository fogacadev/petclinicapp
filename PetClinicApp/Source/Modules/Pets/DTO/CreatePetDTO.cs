using System;
using System.ComponentModel.DataAnnotations;

namespace PetClinicApp.Source.Modules.Pets.DTO
{
    public class CreatePetDTO
    {
        [Required(ErrorMessage = "The name field is required")]
        public string Name { get; set; }
        public DateTime BornIn { get; set; }
        public long AnimalId { get; set; }
    }
}
