using PetClinicApp.Source.Modules.Pets.Entities;
using PetClinicApp.Source.Modules.Reminders.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Reminders.Entities
{
    public class Reminder
    {
        public long Id { get; set; }
        public DateTime ReminderDate { get; set; }
        public ReminderType ReminderType { get; set; }
        public long ReminderTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Pet Pet { get; set; }
        public long PetId { get; set; }
        public bool Finished { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public static class ReminderExtensions
    {
        public static Reminder ToEntity(this ReminderDTO reminder)
        {
            return new Reminder
            {
                Id = reminder.Id,
                ReminderDate = reminder.ReminderDate,
                ReminderTypeId = reminder.ReminderTypeId,
                Title = reminder.Title,
                Description = reminder.Description,
                PetId = reminder.PetId,
                Finished = reminder.Finished,
                CreatedAt = reminder.CreatedAt
            };
        }

        public static ReminderDTO ToDTO(this Reminder reminder)
        {
            return new ReminderDTO
            {
                Id = reminder.Id,
                ReminderDate = reminder.ReminderDate,
                ReminderTypeId = reminder.ReminderTypeId,
                Title = reminder.Title,
                Description = reminder.Description,
                PetId = reminder.PetId,
                Finished = reminder.Finished,
                CreatedAt = reminder.CreatedAt
            };
        }

        public static Reminder ToEntity(this CreateReminderDTO reminder)
        {
            return new Reminder
            {
                ReminderDate = reminder.ReminderDate,
                ReminderTypeId = reminder.ReminderTypeId,
                Title = reminder.Title,
                Description = reminder.Description,
                PetId = reminder.PetId,
            };
        }

        public static Reminder ToEntity(this UpdateReminderDTO reminder)
        {
            return new Reminder
            {
                Id = reminder.Id,
                ReminderDate = reminder.ReminderDate,
                ReminderTypeId = reminder.ReminderTypeId,
                Title = reminder.Title,
                Description = reminder.Description,
            };
        }

        public static IQueryable<Reminder> Filter(this IQueryable<Reminder> query, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToUpper();

                query = query.Where(r => r.Title.ToUpper().Contains(search) || r.Description.ToUpper().Contains(search));
            }

            return query;
        }
    }
}
