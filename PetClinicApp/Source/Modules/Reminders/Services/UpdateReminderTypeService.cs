using PetClinicApp.Source.Modules.Reminders.DTO;
using PetClinicApp.Source.Modules.Reminders.Entities;
using PetClinicApp.Source.Modules.Reminders.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Services;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Reminders.Services
{
   
    public class UpdateReminderTypeService : ServiceBase
    {
        private readonly IReminderTypesRepository reminderTypesRepository;
        public UpdateReminderTypeService(IReminderTypesRepository reminderTypesRepository)
        {
            this.reminderTypesRepository = reminderTypesRepository;
        }

        public async Task ExecuteAsync(UpdateReminderTypeDTO reminderType)
        {
            ValidateModel(reminderType);

            var createdType = await reminderTypesRepository.Find(reminderType.Id);

            if(createdType == null)
            {
                throw new AppErrorException("Reminder type does not exists.", HttpStatusCode.NotFound);
            }

            createdType.Name = reminderType.Name;

            await reminderTypesRepository.Update(createdType);
        }
    }
}
