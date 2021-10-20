using Microsoft.EntityFrameworkCore;
using PetClinicApp.Source.Infra;
using PetClinicApp.Source.Modules.Reminders.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Reminders.Repositories.Implementations
{
    public class ReminderTypesRepository : IReminderTypesRepository
    {

        private readonly AppDbContext appDbContext;
        public ReminderTypesRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<ReminderType> Create(ReminderType reminderType)
        {
            await appDbContext.ReminderTypes.AddAsync(reminderType);
            await appDbContext.SaveChangesAsync();

            appDbContext.Entry(reminderType).State = EntityState.Detached;

            return reminderType;
        }

        public async Task<ReminderType> Delete(long id)
        {
            var reminderType = await Find(id);

            if(reminderType != null)
            {
                appDbContext.ReminderTypes.Remove(reminderType);
                await appDbContext.SaveChangesAsync();
            }
            return reminderType;
        }

        public async Task<ReminderType> Find(long id)
        {
            return await appDbContext
                .ReminderTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

        }

        public async Task<List<ReminderType>> List(string search = "")
        {
            var reminders = await appDbContext
                .ReminderTypes
                .AsNoTracking()
                .ToListAsync();

            return reminders.AsQueryable().Filter(search).ToList();
        }

        public async Task Update(ReminderType reminderType)
        {
            appDbContext.ReminderTypes.Update(reminderType);
            await appDbContext.SaveChangesAsync();
            appDbContext.Entry(reminderType).State = EntityState.Detached;
        }
    }
}
