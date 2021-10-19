using PetClinicApp.Source.Modules.Accounts.DTO;
using PetClinicApp.Source.Modules.Accounts.Entities;
using PetClinicApp.Source.Modules.Accounts.Models;
using PetClinicApp.Source.Modules.Accounts.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Accounts.Services
{
    public class UpdateUserService : ServiceBase
    {
        private readonly IUsersRepository usersRepository;
        public UpdateUserService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task ExecuteAsync(UpdateAccountDTO account, long loggedUserId)
        {
            ValidateModel(account);

            var createdUser = await usersRepository.Find(loggedUserId);
            if(createdUser == null)
            {
                throw new AppErrorException("Account does not exists",HttpStatusCode.NotFound);
            }

            createdUser.BirthDate = account.BirthDate;
            createdUser.FullName = account.FullName;
            createdUser.Email = account.Email;

            await usersRepository.Update(createdUser);
            
        }
    }
}
