using PetClinicApp.Source.Modules.Reminders.DTO;
using PetClinicApp.Source.Modules.Reminders.Entities;
using PetClinicApp.Source.Modules.Reminders.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Reminders.Services
{


    public class ListReminderByUserService
    {
        private readonly IRemindersRepository remindersRepository;
        public ListReminderByUserService(IRemindersRepository remindersRepository)
        {
            this.remindersRepository = remindersRepository;
        }

        public async Task<List<ReminderDTO>> ExecuteAsync(long loggedUserId, string search)
        {
            var reminders =  await remindersRepository.ListByUserId(loggedUserId, search);

            return reminders.Select(r => r.ToDTO()).ToList();
        }
    }
}
