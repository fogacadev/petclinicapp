using PetClinicApp.Source.Modules.MedicalHistories.DTO;
using PetClinicApp.Source.Modules.MedicalHistories.Entities;
using PetClinicApp.Source.Modules.MedicalHistories.Repositories;
using PetClinicApp.Source.Modules.Pets.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Services;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.MedicalHistories.Services
{
    public class CreateMedicalHistoryService : ServiceBase
    {
        private readonly IMedicalHistoriesRepository medicalHistoriesRepository;
        private readonly IMedicalHistoryTypesRepository medicalHistoryTypesRepository;
        private readonly IPetsRepository petsRepository;
        public CreateMedicalHistoryService(IMedicalHistoriesRepository medicalHistoriesRepository,
            IMedicalHistoryTypesRepository medicalHistoryTypesRepository,
            IPetsRepository petsRepository)
        {
            this.medicalHistoriesRepository = medicalHistoriesRepository;
            this.medicalHistoryTypesRepository = medicalHistoryTypesRepository;
            this.petsRepository = petsRepository;
        }

        public async Task<MedicalHistoryDTO> ExecuteAsync(long loggedUserId, CreateMedicalHistoryDTO medicalHistory)
        {
            //verificar se o tipo de historico existe
            var type = await medicalHistoryTypesRepository.Find(medicalHistory.HistoryTypeId);
            if(type == null)
            {
                throw new AppErrorException("Medical history type does not exists.", HttpStatusCode.NotFound);
            }

            var pet = await petsRepository.Find(medicalHistory.PetId);
            if(pet == null)
            {
                throw new AppErrorException("Pet does not exists.", HttpStatusCode.NotFound);
            }

            if(pet.UserId != loggedUserId)
            {
                throw new AppErrorException("You are not allowed to perform this action.", HttpStatusCode.Forbidden);
            }

            ValidateModel(medicalHistory);

            var createdHistory = await medicalHistoriesRepository.Create(medicalHistory.ToEntity());

            return createdHistory.ToDTO();
        }
    }
}
