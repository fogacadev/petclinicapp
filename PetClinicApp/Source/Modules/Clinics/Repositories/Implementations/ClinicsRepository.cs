using Microsoft.EntityFrameworkCore;
using PetClinicApp.Source.Infra;
using PetClinicApp.Source.Modules.Clinics.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Clinics.Repositories.Implementations
{
    public class ClinicsRepository : IClinicsRepository
    {
        private readonly AppDbContext appDbContext;
        public ClinicsRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Clinic> Create(Clinic clinic)
        {
            await appDbContext.Clinics.AddAsync(clinic);
            await appDbContext.SaveChangesAsync();

            appDbContext.Entry(clinic).State = EntityState.Detached;

            return clinic;
        }

        public async Task<Clinic> Delete(long id)
        {
            var clinic = await Find(id);

            if(clinic != null)
            {
                appDbContext.Clinics.Remove(clinic);
                await appDbContext.SaveChangesAsync();
            }

            return clinic;
        }

        public Task<Clinic> Find(long id)
        {
            return appDbContext
                .Clinics
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Clinic>> List(string search = "")
        {
            var clinics = await appDbContext
                .Clinics
                .AsNoTracking()
                .ToListAsync();

            return clinics.AsQueryable().Filter(search).ToList();
        }

        public async Task Update(Clinic clinic)
        {
            appDbContext.Clinics.Update(clinic);
            await appDbContext.SaveChangesAsync();

            appDbContext.Entry(clinic).State = EntityState.Detached;
        }
    }
}
