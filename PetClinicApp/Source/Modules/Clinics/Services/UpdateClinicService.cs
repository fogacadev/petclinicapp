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
    public class UpdateClinicService : ServiceBase
    {
        private readonly IClinicsRepository clinicsRepository;
        public UpdateClinicService(IClinicsRepository clinicsRepository)
        {
            this.clinicsRepository = clinicsRepository;
        }

        public async Task ExecuteAsync(UpdateClinicDTO clinic)
        {
            ValidateModel(clinic);

            var clinicExists = await clinicsRepository.Find(clinic.Id);
            if (clinicExists == null)
            {
                throw new AppErrorException("Clinic does not exists.", HttpStatusCode.NotFound);
            }

            clinicExists.Descripton = clinic.Descripton;
            clinicExists.Name = clinic.Name;
            clinicExists.Address = clinic.Address;
            clinicExists.City = clinic.City;
            clinicExists.ZipCode = clinic.ZipCode;
            clinicExists.State = clinic.State;
            clinicExists.PhoneNumber = clinic.PhoneNumber;

            await clinicsRepository.Update(clinicExists);
        }
    }
}
