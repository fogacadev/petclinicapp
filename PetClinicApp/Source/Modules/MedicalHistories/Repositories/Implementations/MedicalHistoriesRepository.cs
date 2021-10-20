using Microsoft.EntityFrameworkCore;
using PetClinicApp.Source.Infra;
using PetClinicApp.Source.Modules.MedicalHistories.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.MedicalHistories.Repositories.Implementations
{
    public class MedicalHistoriesRepository : IMedicalHistoriesRepository
    {
        private readonly AppDbContext appDbContext;
        public MedicalHistoriesRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<MedicalHistory> Create(MedicalHistory medicalHistory)
        {
            await appDbContext.MedicalHistories.AddAsync(medicalHistory);
            await appDbContext.SaveChangesAsync();

            appDbContext.Entry(medicalHistory).State = EntityState.Detached;

            return medicalHistory;
        }

        public async Task<MedicalHistory> Delete(long id)
        {
            var medicalHistory = await Find(id);

            if(medicalHistory != null)
            {
                appDbContext.MedicalHistories.Remove(medicalHistory);
                await appDbContext.SaveChangesAsync();
            }

            return medicalHistory;
        }

        public async Task<MedicalHistory> Find(long id)
        {
            return await appDbContext
                .MedicalHistories
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<List<MedicalHistory>> List(long petId, string search = "")
        {
            var medicalHistories = await appDbContext
                .MedicalHistories
                .Where(m => m.PetId == petId)
                .AsNoTracking()
                .ToListAsync();

            return medicalHistories.AsQueryable().Filter(search).ToList();
        }

        public async Task Update(MedicalHistory medicalHistory)
        {
            appDbContext.MedicalHistories.Update(medicalHistory);
            await appDbContext.SaveChangesAsync();

            appDbContext.Entry(medicalHistory).State = EntityState.Detached;
        }
    }
}
