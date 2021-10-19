using PetClinicApp.Source.Modules.Clinics.DTO;
using PetClinicApp.Source.Modules.Clinics.Entities;
using PetClinicApp.Source.Modules.Clinics.Repositories;
using PetClinicApp.Source.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Clinics.Services
{
    public class CreateClinicService : ServiceBase
    {
        private readonly IClinicsRepository clinicsRepository;
        public CreateClinicService(IClinicsRepository clinicsRepository)
        {
            this.clinicsRepository = clinicsRepository;
        }

        public async Task<Clinic> ExecuteAsync(ClinicDTO clinic)
        {
            ValidateModel(clinic);

            var createdClinic = await clinicsRepository.Create(clinic.ToEntity());

            return createdClinic;
        }
    }
}
