using PetClinicApp.Source.Modules.MedicalHistories.Entities;
using PetClinicApp.Source.Modules.MedicalHistories.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.MedicalHistories.Services
{
    public class ListMedicalHistoryTypeService
    {
        private readonly IMedicalHistoryTypesRepository medicalHistoryTypesRepository;
        public ListMedicalHistoryTypeService(IMedicalHistoryTypesRepository medicalHistoryTypesRepository)
        {
            this.medicalHistoryTypesRepository = medicalHistoryTypesRepository;
        }

        public async Task<List<MedicalHistoryType>> ExecuteAsync(string search)
        {
            var types = await medicalHistoryTypesRepository.List(search);

            return types;
        }
    }
}
