using PetClinicApp.Source.Modules.Reminders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Reminders.Repositories
{
    public interface IReminderTypesRepository
    {
        Task<ReminderType> Create(ReminderType reminderType);
        Task<ReminderType> Find(long id);
        Task<List<ReminderType>> List(string search = "");
        Task Update(ReminderType reminderType);
        Task<ReminderType> Delete(long id);
    }
}
