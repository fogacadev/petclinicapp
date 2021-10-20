using PetClinicApp.Source.Modules.MedicalHistories.DTO;
using PetClinicApp.Source.Modules.MedicalHistories.Entities;
using PetClinicApp.Source.Modules.MedicalHistories.Repositories;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<MedicalHistoryTypeDTO>> ExecuteAsync(string search)
        {
            var types = await medicalHistoryTypesRepository.List(search);

            return types.Select(t => t.ToDTO()).ToList();
        }
    }
}
