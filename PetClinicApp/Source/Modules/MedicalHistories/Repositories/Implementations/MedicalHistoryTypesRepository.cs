using Microsoft.EntityFrameworkCore;
using PetClinicApp.Source.Infra;
using PetClinicApp.Source.Modules.MedicalHistories.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.MedicalHistories.Repositories.Implementations
{
    public class MedicalHistoryTypesRepository : IMedicalHistoryTypesRepository
    {

        private readonly AppDbContext appDbContext;
        public MedicalHistoryTypesRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<MedicalHistoryType> Create(MedicalHistoryType medicalHistoryType)
        {
            await appDbContext.MedicalHistoryTypes.AddAsync(medicalHistoryType);
            await appDbContext.SaveChangesAsync();

            appDbContext.Entry(medicalHistoryType).State = EntityState.Detached;

            return medicalHistoryType;
        }

        public async Task<MedicalHistoryType> Delete(long id)
        {
            var type = await Find(id);

            if(type != null)
            {
                appDbContext.MedicalHistoryTypes.Remove(type);
                await appDbContext.SaveChangesAsync();
            }

            return type;
        }

        public async Task<MedicalHistoryType> Find(long id)
        {
            return await appDbContext.MedicalHistoryTypes.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<MedicalHistoryType>> List(string search = "")
        {
            var types = await appDbContext.MedicalHistoryTypes.AsNoTracking().ToListAsync();

            return types.AsQueryable().Filter(search).ToList();
        }

        public async Task Update(MedicalHistoryType medicalHistoryType)
        {
            appDbContext.MedicalHistoryTypes.Update(medicalHistoryType);
            await appDbContext.SaveChangesAsync();

            appDbContext.Entry(medicalHistoryType).State = EntityState.Detached;
        }
    }
}
