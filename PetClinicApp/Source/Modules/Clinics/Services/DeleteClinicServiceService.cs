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
    public class DeleteClinicServiceService : ServiceBase
    {
        private readonly IClinicServicesRepository clinicServicesRepository;
        public DeleteClinicServiceService(IClinicServicesRepository clinicServicesRepository)
        {
            this.clinicServicesRepository = clinicServicesRepository;
        }

        public async Task<ClinicServiceDTO> ExecuteAsync(long id)
        {
            var clinicService = await clinicServicesRepository.Find(id);

            if(clinicService == null)
            {
                throw new AppErrorException("Service does not exists", HttpStatusCode.NotFound);
            }

            await clinicServicesRepository.Delete(id);

            return clinicService.ToDTO();
        }
    }
}
