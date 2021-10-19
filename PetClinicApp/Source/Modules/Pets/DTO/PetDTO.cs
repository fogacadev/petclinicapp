using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Pets.DTO
{
    public class PetDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime BornIn { get; set; }
        public long AnimalId { get; set; }
        public long UserId { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
