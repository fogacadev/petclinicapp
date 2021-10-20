using PetClinicApp.Source.Modules.MedicalHistories.Entities;
using PetClinicApp.Source.Modules.MedicalHistories.Repositories;
using PetClinicApp.Source.Modules.Pets.Repositories;
using PetClinicApp.Source.Modules.Pets.Repositories.Implementations;
using PetClinicApp.Source.Shared.Errors;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.MedicalHistories.Services
{
    public class ListMedicalHistoriesService
    {
        private readonly IMedicalHistoriesRepository medicalHistoriesRepository;
        private readonly IPetsRepository petsRepository;
        public ListMedicalHistoriesService(IMedicalHistoriesRepository medicalHistoriesRepository,
            IPetsRepository petsRepository)
        {
            this.medicalHistoriesRepository = medicalHistoriesRepository;
            this.petsRepository = petsRepository;
        }

        public async Task<List<MedicalHistory>> ExecuteAsync(long loggedUserId, long petId, string search)
        {
            var pet = await petsRepository.Find(petId);

            if(pet.UserId != loggedUserId)
            {
                throw new AppErrorException("You are not allowed to perform this action.", HttpStatusCode.Forbidden);
            }

            var medicalHistories = await medicalHistoriesRepository.List(petId, search);

            return medicalHistories;
        }
    }
}
