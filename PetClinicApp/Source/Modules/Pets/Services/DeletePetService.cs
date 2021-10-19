﻿using PetClinicApp.Source.Modules.Pets.Entities;
using PetClinicApp.Source.Modules.Pets.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Pets.Services
{
    public class DeletePetService : ServiceBase
    {
        private readonly IPetsRepository petsRepository;
        public DeletePetService(IPetsRepository petsRepository)
        {
            this.petsRepository = petsRepository;
        }


        public async Task<Pet> ExecuteAsync(long id)
        {
            var petExists = await petsRepository.Find(id);

            if (petExists == null)
            {
                throw new AppErrorException("Pet does not exists", HttpStatusCode.NotFound);
            }

            await petsRepository.Delete(id);

            return petExists;
        }
    }
}
