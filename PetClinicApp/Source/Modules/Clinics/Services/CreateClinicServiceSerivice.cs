using PetClinicApp.Source.Modules.Clinics.DTO;
using PetClinicApp.Source.Modules.Clinics.Entities;
using PetClinicApp.Source.Modules.Clinics.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Clinics.Services
{
    public class CreateClinicServiceSerivice : ServiceBase
    {
        private readonly IClinicServicesRepository clinicServicesRepository;
        private readonly IClinicsRepository clinicsRepository;
        public CreateClinicServiceSerivice(IClinicServicesRepository clinicServicesRepository, IClinicsRepository clinicsRepository)
        {
            this.clinicServicesRepository = clinicServicesRepository;
            this.clinicsRepository = clinicsRepository;
        }

        public async Task<ClinicServiceDTO> ExecuteAsync(CreateClinicServiceDTO clinicService)
        {
            ValidateModel(clinicService);

            var clinic = await clinicsRepository.Find(clinicService.ClinicId);
            if(clinic == null)
            {
                throw new AppErrorException("Clinic does not exists.");
            }
            var createdClinicService = await clinicServicesRepository.Create(clinicService.ToEntity());

            return createdClinicService.ToDTO();
        }
    }
}
