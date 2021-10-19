using PetClinicApp.Source.Modules.Pets.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Pets.Entities
{
    public class Animal
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public static class AnimalExtensions
    {
        public static Animal ToEntity(this AnimalDTO animal)
        {
            return new Animal
            {
                Id = animal.Id,
                Name = animal.Name
            };
        }

        public static AnimalDTO ToDTO(this Animal animal)
        {
            return new AnimalDTO
            {
                Id = animal.Id,
                Name = animal.Name
            };
        }
    }
}
