using System.ComponentModel.DataAnnotations;

namespace PetClinicApp.Source.Modules.Clinics.DTO
{
    public class CreateClinicServiceDTO
    {
        [Required(ErrorMessage = "The name field is required")]
        public string Name { get; set; }
        public long ClinicId { get; set; }
    }
}
