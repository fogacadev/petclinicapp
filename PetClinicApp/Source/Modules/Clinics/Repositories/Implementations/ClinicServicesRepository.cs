using Microsoft.EntityFrameworkCore;
using PetClinicApp.Source.Infra;
using PetClinicApp.Source.Modules.Clinics.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Clinics.Repositories.Implementations
{
    public class ClinicServicesRepository : IClinicServicesRepository
    {

        private readonly AppDbContext appDbContext;
        public ClinicServicesRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<ClinicService> Create(ClinicService clinicService)
        {
            await appDbContext.ClinicServices.AddAsync(clinicService);
            await appDbContext.SaveChangesAsync();

            appDbContext.Entry(clinicService).State = EntityState.Detached;

            return clinicService;
        }

        public async Task<ClinicService> Delete(long id)
        {
            var service = await Find(id);
            if(service != null)
            {
                appDbContext.ClinicServices.Remove(service);
                await appDbContext.SaveChangesAsync();
            }

            return service;
        }

        public async Task<ClinicService> Find(long id)
        {
            return await appDbContext
                .ClinicServices
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<ClinicService>> List(long clinicId, string search = "")
        {
            var clinicServices = await appDbContext
                .ClinicServices
                .Where(s => s.ClinicId == clinicId)
                .AsNoTracking()
                .ToListAsync();

            return clinicServices.AsQueryable().Filter(search).ToList();
        }

        public async Task Update(ClinicService clinicService)
        {
            appDbContext.Update(clinicService);
            await appDbContext.SaveChangesAsync();
        }
    }
}
