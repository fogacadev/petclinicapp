using PetClinicApp.Source.Modules.Clinics.Entities;
using PetClinicApp.Source.Modules.Clinics.Repositories;
using PetClinicApp.Source.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Clinics.Services
{
    public class FindClinicServicesService
    {
        private readonly IClinicServicesRepository clinicServicesRepository;
        public FindClinicServicesService(IClinicServicesRepository clinicServicesRepository)
        {
            this.clinicServicesRepository = clinicServicesRepository;
        }

        public async Task<ClinicService> ExecuteAsync(long id)
        {
            var service = await clinicServicesRepository.Find(id);

            if(service == null)
            {
                throw new AppErrorException("Service does not exists", HttpStatusCode.NotFound);
            }

            return service;
        }
    }
}
