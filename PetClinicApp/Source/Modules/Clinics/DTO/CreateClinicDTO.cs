using System.ComponentModel.DataAnnotations;

namespace PetClinicApp.Source.Modules.Clinics.DTO
{
    public class CreateClinicDTO
    {
        [Required(ErrorMessage= "The name field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The description field is mandatory")]
        public string Descripton { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
    }
}
