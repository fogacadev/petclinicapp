using PetClinicApp.Source.Modules.Reminders.DTO;
using PetClinicApp.Source.Modules.Reminders.Entities;
using PetClinicApp.Source.Modules.Reminders.Repositories;
using PetClinicApp.Source.Shared.Services;
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

        public async Task ExecuteAsync(ReminderTypeDTO reminderType)
        {
            ValidateModel(reminderType);

            await reminderTypesRepository.Update(reminderType.ToModel());
        }
    }
}
