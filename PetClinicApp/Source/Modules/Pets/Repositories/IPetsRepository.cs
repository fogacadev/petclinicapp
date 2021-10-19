using PetClinicApp.Source.Modules.Pets.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Pets.Repositories
{
    public interface IPetsRepository
    {
        Task<Pet> Create(Pet pet);
        Task<Pet> Find(long id);
        Task<List<Pet>> List(long userId, string search = "");
        Task<Pet> Delete(long id);
        Task Update(Pet pet);
    }
}
