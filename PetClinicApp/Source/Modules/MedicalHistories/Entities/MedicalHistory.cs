using PetClinicApp.Source.Modules.Clinics.Entities;
using PetClinicApp.Source.Modules.MedicalHistories.DTO;
using PetClinicApp.Source.Modules.Pets.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.MedicalHistories.Entities
{
    public class MedicalHistory
    {
        public long Id { get; set; }
        public long HistoryTypeId { get; set; }
        public MedicalHistoryType HistoryType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Pet Pet { get; set; }
        public long PetId { get; set; }
        public Clinic Clinic { get; set; }
        public long? ClinicId { get; set; }
        public string Attachment { get; set; }
    }

    public static class MedicalHistoryExtensions
    {
        public static MedicalHistory ToModel(this MedicalHistoryDTO medicalHistory)
        {
            return new MedicalHistory
            {
                Id = medicalHistory.Id,
                HistoryTypeId = medicalHistory.HistoryTypeId,
                Title = medicalHistory.Title,
                Description = medicalHistory.Description,
                PetId = medicalHistory.PetId,
                ClinicId = medicalHistory.ClinicId,
                Attachment = medicalHistory.Attachment
            };
        }

        public static MedicalHistoryDTO ToModel(this MedicalHistory medicalHistory)
        {
            return new MedicalHistoryDTO
            {
                Id = medicalHistory.Id,
                HistoryTypeId = medicalHistory.HistoryTypeId,
                Title = medicalHistory.Title,
                Description = medicalHistory.Description,
                PetId = medicalHistory.PetId,
                ClinicId = medicalHistory.ClinicId,
                Attachment = medicalHistory.Attachment
            };
        }
    }
}
