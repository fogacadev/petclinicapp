using System.ComponentModel.DataAnnotations;

namespace PetClinicApp.Source.Modules.Pets.DTO
{
    public class CreateAnimalDTO
    {
        [Required(ErrorMessage = "The name field is required")]
        public string Name { get; set; }
    }
}
