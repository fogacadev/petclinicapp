using PetClinicApp.Source.Modules.Reminders.DTO;
using PetClinicApp.Source.Modules.Reminders.Entities;
using PetClinicApp.Source.Modules.Reminders.Repositories;
using PetClinicApp.Source.Shared.Services;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Reminders.Services
{
    public class CreateReminderTypeService : ServiceBase
    {
        private readonly IReminderTypesRepository reminderTypesRepository;
        public CreateReminderTypeService(IReminderTypesRepository reminderTypesRepository)
        {
            this.reminderTypesRepository = reminderTypesRepository;
        }

        public async Task<ReminderTypeDTO> ExecuteAsync(CreateReminderTypeDTO reminderType)
        {
            ValidateModel(reminderType);

            var createdReminder = await reminderTypesRepository.Create(reminderType.ToEntity());

            return createdReminder.ToDTO();
        }
    }
}
