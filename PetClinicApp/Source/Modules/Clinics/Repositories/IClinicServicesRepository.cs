using PetClinicApp.Source.Modules.Clinics.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Clinics.Repositories
{
    public interface IClinicServicesRepository
    {
        Task<ClinicService> Create(ClinicService clinicService);
        Task<ClinicService> Find(long id);
        Task<List<ClinicService>> List(long clinicId, string search = "");
        Task<ClinicService> Delete(long id);
        Task Update(ClinicService clinicService);
    }
}
