using PetClinicApp.Source.Modules.MedicalHistories.DTO;
using PetClinicApp.Source.Modules.MedicalHistories.Entities;
using PetClinicApp.Source.Modules.MedicalHistories.Repositories;
using PetClinicApp.Source.Modules.Pets.Repositories;
using PetClinicApp.Source.Shared.Errors;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.MedicalHistories.Services
{
    public class FindMedicalHistoryService
    {

        private readonly IMedicalHistoriesRepository medicalHistoriesRepository;
        private readonly IPetsRepository petsRepository;
        public FindMedicalHistoryService(IMedicalHistoriesRepository medicalHistoriesRepository,
            IPetsRepository petsRepository)
        {
            this.medicalHistoriesRepository = medicalHistoriesRepository;
            this.petsRepository = petsRepository;
        }

        public async Task<MedicalHistoryDTO> ExecuteAsync(long loggedUserId, long id)
        {
            var history = await medicalHistoriesRepository.Find(id);

            if(history == null)
            {
                throw new AppErrorException("History does not exists.", HttpStatusCode.NotFound);
            }

            var pet = await petsRepository.Find(history.PetId);
            if(pet.UserId != loggedUserId)
            {
                throw new AppErrorException("You are not allowed to perform this action.", HttpStatusCode.Forbidden);
            }

            return history.ToDTO();
        }
    }
}
