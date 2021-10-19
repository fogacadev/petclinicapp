using PetClinicApp.Source.Modules.Reminders.Entities;
using PetClinicApp.Source.Modules.Reminders.Repositories;
using PetClinicApp.Source.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Reminders.Services
{
    public class FindReminderService
    {
        private readonly IRemindersRepository remindersRepository;
        public FindReminderService(IRemindersRepository remindersRepository)
        {
            this.remindersRepository = remindersRepository;
        }

        public async Task<Reminder> ExecuteAsync(long id)
        {
            var reminder = await remindersRepository.Find(id);

            if(reminder == null)
            {
                throw new AppErrorException("Reminder does not exists", HttpStatusCode.OK);
            }

            return reminder;
        }
    }
}
