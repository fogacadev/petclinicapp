using PetClinicApp.Source.Modules.Pets.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Pets.Repositories
{
    public interface IAnimalsRepository
    {
        Task<Animal> Create(Animal animal);
        Task<Animal> Find(long id);
        Task<List<Animal>> List(string search = "");
        Task<Animal> Delete(long id);
        Task Update(Animal animal);
        Task<bool> InUse(long id);
    }
}
