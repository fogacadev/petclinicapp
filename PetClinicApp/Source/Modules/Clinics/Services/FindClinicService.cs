using PetClinicApp.Source.Modules.Clinics.DTO;
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
    public class FindClinicService
    {
        private readonly IClinicsRepository clinicsRepository;
        public FindClinicService(IClinicsRepository clinicsRepository)
        {
            this.clinicsRepository = clinicsRepository;
        }

        public async Task<ClinicDTO> ExecuteAsync(long id)
        {
            var clinic = await clinicsRepository.Find(id);

            if(clinic == null)
            {
                throw new AppErrorException("Clinic does not exists", HttpStatusCode.NotFound);
            }

            return clinic.ToDTO();
        }
    }
}
