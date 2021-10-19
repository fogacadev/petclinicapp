using PetClinicApp.Source.Modules.Reminders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Reminders.Repositories
{
    public interface IRemindersRepository
    {
        Task<Reminder> Create(Reminder reminder);
        Task<Reminder> Find(long id);
        Task<List<Reminder>> ListByPetId(long petId, string search = "");
        Task<List<Reminder>> ListByUserId(long userId, string search = "");
        Task<Reminder> Delete(long id);
        Task Update(Reminder reminder);
    }
}
