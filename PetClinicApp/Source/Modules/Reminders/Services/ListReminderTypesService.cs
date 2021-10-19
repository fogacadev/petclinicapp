using PetClinicApp.Source.Modules.Reminders.Entities;
using PetClinicApp.Source.Modules.Reminders.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Reminders.Services
{

    public class ListReminderTypesService
    {
        private readonly IReminderTypesRepository reminderTypesRepository;
        public ListReminderTypesService(IReminderTypesRepository reminderTypesRepository)
        {
            this.reminderTypesRepository = reminderTypesRepository;
        }

        public async Task<List<ReminderType>> ExecuteAsync(string search)
        {
            return await reminderTypesRepository.List(search);
        }
    }
}
