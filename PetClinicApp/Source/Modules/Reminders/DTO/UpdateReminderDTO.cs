using System;
using System.ComponentModel.DataAnnotations;

namespace PetClinicApp.Source.Modules.Reminders.DTO
{
    public class UpdateReminderDTO
    {
        public long Id { get; set; }
        public DateTime ReminderDate { get; set; }
        public long ReminderTypeId { get; set; }
        [Required(ErrorMessage = "The title field is mandatory")]
        [MinLength(3, ErrorMessage = "The title field must have at least 3 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The description field is mandatory")]
        [MinLength(3, ErrorMessage = "The description field must have at least 3 characters")]
        public string Description { get; set; }
    }
}
