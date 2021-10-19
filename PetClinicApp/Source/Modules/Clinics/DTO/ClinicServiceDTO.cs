using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Clinics.DTO
{
    public class ClinicServiceDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ClinicId { get; set; }
    }
}
