using PetClinicApp.Source.Modules.Pets.Repositories;
using PetClinicApp.Source.Modules.Reminders.DTO;
using PetClinicApp.Source.Modules.Reminders.Entities;
using PetClinicApp.Source.Modules.Reminders.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Reminders.Services
{
    public class UpdateReminderService : ServiceBase
    {
        private readonly IRemindersRepository remindersRepository;
        private readonly IPetsRepository petsRepository;
        private readonly IReminderTypesRepository reminderTypesRepository;
        public UpdateReminderService(IRemindersRepository remindersRepository,
            IPetsRepository petsRepository,
            IReminderTypesRepository reminderTypesRepository)
        {
            this.remindersRepository = remindersRepository;
            this.petsRepository = petsRepository;
            this.reminderTypesRepository = reminderTypesRepository;
        }

        public async Task ExecuteAsync(ReminderDTO reminderDTO)
        {
            ValidateModel(reminderDTO);

            var reminder = reminderDTO.ToEntity();

            var createdReminder = await remindersRepository.Find(reminder.Id);

            if(createdReminder == null)
            {
                throw new AppErrorException("Reminder does not exists", HttpStatusCode.NotFound);
            }

            var petExists = await petsRepository.Find(reminder.PetId);
            if (petExists == null)
            {
                throw new AppErrorException("Pet does not exists", HttpStatusCode.NotFound);
            }


            var reminderTypeExists = await reminderTypesRepository.Find(reminder.ReminderTypeId);
            if (reminderTypeExists == null)
            {
                throw new AppErrorException("Reminder type does not exists", HttpStatusCode.NotFound);
            }


            reminder.PetId = createdReminder.PetId;
            reminder.CreatedAt = createdReminder.CreatedAt;
            reminder.Finished = createdReminder.Finished;

            await remindersRepository.Update(reminder);
        }
    }
}
