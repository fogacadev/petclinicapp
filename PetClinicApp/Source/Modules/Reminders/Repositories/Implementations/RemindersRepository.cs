using Microsoft.EntityFrameworkCore;
using PetClinicApp.Source.Infra;
using PetClinicApp.Source.Modules.Reminders.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Reminders.Repositories.Implementations
{
    public class RemindersRepository : IRemindersRepository
    {
        private readonly AppDbContext appDbContext;
        public RemindersRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Reminder> Create(Reminder reminder)
        {
            await appDbContext.Reminders.AddAsync(reminder);
            await appDbContext.SaveChangesAsync();

            appDbContext.Entry(reminder).State = EntityState.Detached;

            return reminder;
        }

        public async Task<Reminder> Delete(long id)
        {
            var reminder = await Find(id);

            if(reminder != null)
            {
                appDbContext.Reminders.Remove(reminder);
                await appDbContext.SaveChangesAsync();
            }

            return reminder;
        }

        public Task<Reminder> Find(long id)
        {
            return appDbContext
                .Reminders
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Reminder>> ListByPetId(long petId, string search = "")
        {
            var reminders = await appDbContext.Reminders.Where(p => p.PetId == petId).AsNoTracking().ToListAsync();

            return reminders.AsQueryable().Filter(search).ToList();
        }

        public async Task<List<Reminder>> ListByUserId(long userId, string search = "")
        {
            var reminders = await appDbContext
                .Reminders
                .Where(r => r.Pet.UserId == userId)
                .AsNoTracking()
                .ToListAsync();

            return reminders.AsQueryable().Filter(search).ToList();
        }

        public async Task Update(Reminder reminder)
        {
            appDbContext.Reminders.Update(reminder);
            await appDbContext.SaveChangesAsync();

            appDbContext.Entry(reminder).State = EntityState.Detached;
        }
    }
}
