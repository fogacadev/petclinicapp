using PetClinicApp.Source.Modules.Clinics.Entities;
using PetClinicApp.Source.Modules.Clinics.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Clinics.Services
{
    public class ListClinicServicesUseCase
    {
        private readonly IClinicServicesRepository clinicServicesRepository;
        public ListClinicServicesUseCase(IClinicServicesRepository clinicServicesRepository)
        {
            this.clinicServicesRepository = clinicServicesRepository;
        }

        public async Task<List<ClinicService>> Execute(long clinicId, string search)
        {
            var services = await clinicServicesRepository.List(clinicId, search);

            return services;
        }
    }
}
