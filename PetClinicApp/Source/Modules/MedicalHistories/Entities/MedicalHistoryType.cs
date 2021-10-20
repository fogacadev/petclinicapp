using PetClinicApp.Source.Modules.MedicalHistories.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.MedicalHistories.Entities
{
    public class MedicalHistoryType
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public static class MedicalHistoryTypeExtensions
    {
        public static MedicalHistoryType ToEntity(this MedicalHistoryTypeDTO medicalHistoryType)
        {
            return new MedicalHistoryType
            {
                Id = medicalHistoryType.Id,
                Name = medicalHistoryType.Name
            };
        }

        public static MedicalHistoryTypeDTO ToDTO(this MedicalHistoryType medicalHistoryType)
        {
            return new MedicalHistoryTypeDTO
            {
                Id = medicalHistoryType.Id,
                Name = medicalHistoryType.Name
            };
        }

        public static IQueryable<MedicalHistoryType> Filter(this IQueryable<MedicalHistoryType> query, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToUpper();
                query = query.Where(t => t.Name.ToUpper().Contains(search));
            }

            return query;
        }
    }
}
