using PetClinicApp.Source.Modules.Reminders.DTO;
using PetClinicApp.Source.Modules.Reminders.Entities;
using PetClinicApp.Source.Modules.Reminders.Repositories;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<ReminderTypeDTO>> ExecuteAsync(string search)
        {
            var reminderTypes =  await reminderTypesRepository.List(search);

            return reminderTypes.Select(r => r.ToDTO()).ToList();
        }
    }
}
