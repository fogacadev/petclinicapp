using PetClinicApp.Source.Modules.MedicalHistories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.MedicalHistories.Repositories
{
    public interface IMedicalHistoriesRepository
    {
        Task<MedicalHistory> Create(MedicalHistory medicalHistory);
        Task<MedicalHistory> Find(long id);
        Task<List<MedicalHistory>> List(long petId, string search = "");
        Task<MedicalHistory> Delete(long id);
        Task Update(MedicalHistory medicalHistory);
    }
}
