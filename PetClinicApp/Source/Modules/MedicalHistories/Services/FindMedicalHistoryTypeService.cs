using PetClinicApp.Source.Modules.MedicalHistories.Entities;
using PetClinicApp.Source.Modules.MedicalHistories.Repositories;
using PetClinicApp.Source.Shared.Errors;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.MedicalHistories.Services
{
    public class FindMedicalHistoryTypeService
    {
        private IMedicalHistoryTypesRepository medicalHistoryTypesRepository;
        public FindMedicalHistoryTypeService(IMedicalHistoryTypesRepository medicalHistoryTypesRepository)
        {
            this.medicalHistoryTypesRepository = medicalHistoryTypesRepository;
        }

        public async Task<MedicalHistoryType> ExecuteAsync(long id)
        {
            var type = await medicalHistoryTypesRepository.Find(id);

            if(type == null)
            {
                throw new AppErrorException("Type does not exists.", HttpStatusCode.NotFound);
            }

            return type;
        }
    }
}
