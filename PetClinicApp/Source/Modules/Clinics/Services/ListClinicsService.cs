using PetClinicApp.Source.Modules.Clinics.Entities;
using PetClinicApp.Source.Modules.Clinics.Repositories;
using PetClinicApp.Source.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Clinics.Services
{
    public class ListClinicsService : ServiceBase
    {
        private readonly IClinicsRepository clinicsRepository;
        public ListClinicsService(IClinicsRepository clinicsRepository)
        {
            this.clinicsRepository = clinicsRepository;
        }

        public async Task<List<Clinic>> ExecuteAsync(string search)
        {
            return await clinicsRepository.List(search);
        }
    }
}
