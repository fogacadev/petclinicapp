using PetClinicApp.Source.Modules.MedicalHistories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.MedicalHistories.Repositories
{
    public interface IMedicalHistoryTypesRepository 
    {
        Task<MedicalHistoryType> Create(MedicalHistoryType medicalHistoryType);
        Task<MedicalHistoryType> Find(long id);
        Task<List<MedicalHistoryType>> List(string search = "");
        Task<MedicalHistoryType> Delete(long id);
        Task Update(MedicalHistoryType medicalHistoryType);
    }
}
