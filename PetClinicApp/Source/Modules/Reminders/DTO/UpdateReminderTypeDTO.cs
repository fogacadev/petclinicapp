using System.ComponentModel.DataAnnotations;

namespace PetClinicApp.Source.Modules.Reminders.DTO
{
    public class UpdateReminderTypeDTO
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "The name field is required")]
        public string Name { get; set; }
    }
}
