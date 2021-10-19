using PetClinicApp.Source.Modules.MedicalHistories.Entities;
using PetClinicApp.Source.Modules.MedicalHistories.Repositories;
using PetClinicApp.Source.Modules.Pets.Repositories;
using PetClinicApp.Source.Shared.Errors;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.MedicalHistories.Services
{
    public class DeleteMedicalHistoryService
    {
        private readonly IMedicalHistoriesRepository medicalHistoriesRepository;
        private readonly IPetsRepository petsRepository;
        public DeleteMedicalHistoryService(IMedicalHistoriesRepository medicalHistoriesRepository,
            IPetsRepository petsRepository)
        {
            this.medicalHistoriesRepository = medicalHistoriesRepository;
            this.petsRepository = petsRepository;
        }

        public async Task<MedicalHistory> ExecuteAsync(long loggedUserId, long id)
        {
            var medicalHistory = await medicalHistoriesRepository.Find(id);

            if(medicalHistory == null)
            {
                throw new AppErrorException("Medical history does not exists.", HttpStatusCode.NotFound);
            }

            var pet = await petsRepository.Find(medicalHistory.PetId);
            if(pet.UserId != loggedUserId)
            {
                throw new AppErrorException("You are not allowed to perform this action.", HttpStatusCode.Forbidden);
            }

            await medicalHistoriesRepository.Delete(id);

            return medicalHistory;
        }
    }
}
