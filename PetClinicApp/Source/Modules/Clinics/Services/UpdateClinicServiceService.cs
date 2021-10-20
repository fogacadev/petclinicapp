using PetClinicApp.Source.Modules.Clinics.DTO;
using PetClinicApp.Source.Modules.Clinics.Entities;
using PetClinicApp.Source.Modules.Clinics.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Clinics.Services
{
    public class UpdateClinicServiceService : ServiceBase
    {
        private readonly IClinicServicesRepository clinicServicesRepository;
        public UpdateClinicServiceService(IClinicServicesRepository clinicServicesRepository)
        {
            this.clinicServicesRepository = clinicServicesRepository;
        }

        public async Task ExecuteAsync(UpdateClinicServiceDTO clinicService)
        {
            var createdService = await clinicServicesRepository.Find(clinicService.Id);

            if(createdService == null)
            {
                throw new AppErrorException("Service does not exists", HttpStatusCode.NotFound);
            }

            ValidateModel(clinicService);

            createdService.Name = clinicService.Name;

            await clinicServicesRepository.Update(createdService);
        }
    }
}
