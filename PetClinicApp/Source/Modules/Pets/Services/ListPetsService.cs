using PetClinicApp.Source.Modules.Pets.Entities;
using PetClinicApp.Source.Modules.Pets.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Pets.Services
{
    public class ListPetsService
    {
        private readonly IPetsRepository petsRepository;
        public ListPetsService(IPetsRepository petsRepository)
        {
            this.petsRepository = petsRepository;
        }

        public async Task<List<Pet>> ExecuteAsync(long userId, string search)
        {
            var pets = await petsRepository.List(userId, search);

            return pets;
        }
    }
}
