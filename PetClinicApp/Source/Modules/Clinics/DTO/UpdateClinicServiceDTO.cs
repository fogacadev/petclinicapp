using System.ComponentModel.DataAnnotations;

namespace PetClinicApp.Source.Modules.Clinics.DTO
{
    public class UpdateClinicServiceDTO
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "The name field is required")]
        public string Name { get; set; }
    }
}
