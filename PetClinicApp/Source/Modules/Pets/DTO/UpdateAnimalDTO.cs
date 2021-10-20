using System.ComponentModel.DataAnnotations;

namespace PetClinicApp.Source.Modules.Pets.DTO
{
    public class UpdateAnimalDTO
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "The name field is required")]
        public string Name { get; set; }
    }
}
