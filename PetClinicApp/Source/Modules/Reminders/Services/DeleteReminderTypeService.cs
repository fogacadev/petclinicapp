using PetClinicApp.Source.Modules.Reminders.Entities;
using PetClinicApp.Source.Modules.Reminders.Repositories;
using PetClinicApp.Source.Shared.Errors;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Reminders.Services
{
    public class DeleteReminderTypeService
    {
        private readonly IReminderTypesRepository reminderTypesRepository;
        public DeleteReminderTypeService(IReminderTypesRepository reminderTypesRepository)
        {
            this.reminderTypesRepository = reminderTypesRepository;
        }

        public async Task<ReminderType> ExecuteAsync(long loggedUserId, long id)
        {
            var reminder = await reminderTypesRepository.Find(id);
            if(reminder == null)
            {
                throw new AppErrorException("Reminder type does not exists.", HttpStatusCode.NotFound);
            }

            return await reminderTypesRepository.Delete(id);
        }
    }
}
