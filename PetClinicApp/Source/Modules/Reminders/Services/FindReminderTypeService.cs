using PetClinicApp.Source.Modules.Reminders.Entities;
using PetClinicApp.Source.Modules.Reminders.Repositories;
using PetClinicApp.Source.Shared.Errors;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Reminders.Services
{
    public class FindReminderTypeService
    {
        private readonly IReminderTypesRepository reminderTypesRepository;
        public FindReminderTypeService(IReminderTypesRepository reminderTypesRepository)
        {
            this.reminderTypesRepository = reminderTypesRepository;
        }

        public async Task<ReminderType> ExecuteAsync(long id)
        {
            var reminderType = await reminderTypesRepository.Find(id);

            if(reminderType == null)
            {
                throw new AppErrorException("Reminder type does not exists", HttpStatusCode.NotFound);
            }

            return reminderType;
        }
    }
}
