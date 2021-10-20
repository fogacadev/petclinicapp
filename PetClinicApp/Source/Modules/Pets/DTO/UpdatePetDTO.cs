using System;

namespace PetClinicApp.Source.Modules.Pets.DTO
{
    public class UpdatePetDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime BornIn { get; set; }
        public long AnimalId { get; set; }
    }
}
