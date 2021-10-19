using PetClinicApp.Source.Modules.Clinics.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Clinics.Entities
{
    public class ClinicService
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ClinicId { get; set; }
        public Clinic Clinic { get; set; }
    }

    public static class ClinicServiceExtensios
    {
        public static ClinicService ToEntity(this ClinicServiceDTO service)
        {
            return new ClinicService
            {
                Id = service.Id,
                ClinicId = service.ClinicId,
                Name = service.Name
            };
        }

        public static ClinicServiceDTO ToDTO(this ClinicService service)
        {
            return new ClinicServiceDTO
            {
                Id = service.Id,
                ClinicId = service.ClinicId,
                Name = service.Name
            };
        }
    }
}
