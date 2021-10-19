using PetClinicApp.Source.Modules.Clinics.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Clinics.Entities
{
    public class Clinic
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Descripton { get; set; }
        public string Avatar { get; set; }

        public List<ClinicService> Services { get; set; }
    }

    public static class ClinicExtensions
    {
        public static Clinic ToEntity(this ClinicDTO clinic)
        {
            return new Clinic
            {
                Id = clinic.Id,
                Name = clinic.Name,
                Descripton = clinic.Descripton,
                Avatar = clinic.Avatar
            };
        }

        public static ClinicDTO ToDTO(this Clinic clinic)
        {
            return new ClinicDTO
            {
                Id = clinic.Id,
                Name = clinic.Name,
                Descripton = clinic.Descripton,
                Avatar = clinic.Avatar
            };
        }
    }
}
