using PetClinicApp.Source.Modules.Reminders.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Reminders.Entities
{
    public class ReminderType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public static class ReminderTypeExtensions
    {
        public static ReminderType ToEntity(this ReminderTypeDTO reminderType)
        {
            return new ReminderType
            {
                Id = reminderType.Id,
                Name = reminderType.Name
            };
        }

        public static ReminderType ToEntity(this CreateReminderTypeDTO reminderType)
        {
            return new ReminderType
            {
                Name = reminderType.Name
            };
        }

        public static ReminderType ToEntity(this UpdateReminderTypeDTO reminderType)
        {
            return new ReminderType
            {
                Id = reminderType.Id,
                Name = reminderType.Name
            };
        }

        public static ReminderTypeDTO ToDTO(this ReminderType reminderType)
        {
            return new ReminderTypeDTO
            {
                Id = reminderType.Id,
                Name = reminderType.Name
            };
        }

        public static IQueryable<ReminderType> Filter(this IQueryable<ReminderType> query, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToUpper();

                query = query.Where(r => r.Name.ToUpper().Contains(search));
            }

            return query;
        }
    }
}
