using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Reminders.DTO
{
    public class ReminderDTO
    {
        public long Id { get; set; }
        public DateTime ReminderDate { get; set; }
        public long ReminderTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long PetId { get; set; }
        public bool Finished { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
