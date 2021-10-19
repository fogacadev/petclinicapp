using PetClinicApp.Source.Modules.Accounts.Entities;
using PetClinicApp.Source.Modules.Pets.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Pets.Entities
{
    public class Pet
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime BornIn { get; set; }
        public Animal Animal { get; set; }
        public long AnimalId { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public static class PetExtensions
    {
        public static Pet ToEntity(this PetDTO pet)
        {
            return new Pet
            {
                Id = pet.Id,
                Name = pet.Name,
                BornIn = pet.BornIn,
                AnimalId = pet.AnimalId,
                UserId = pet.UserId,
                Avatar = pet.Avatar,
                CreatedAt = pet.CreatedAt
            };
        }

        public static PetDTO ToDTO(this Pet pet)
        {
            return new PetDTO
            {
                Id = pet.Id,
                Name = pet.Name,
                BornIn = pet.BornIn,
                AnimalId = pet.AnimalId,
                UserId = pet.UserId,
                Avatar = pet.Avatar,
                CreatedAt = pet.CreatedAt
            };
        }
    }
} 
