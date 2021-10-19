using PetClinicApp.Source.Modules.Clinics.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Clinics.Repositories
{
    public interface IClinicsRepository
    {
        Task<Clinic> Create(Clinic clinic);
        Task<Clinic> Find(long id);
        Task<List<Clinic>> List(string search = "");
        Task<Clinic> Delete(long id);
        Task Update(Clinic clinic);
    }
}
