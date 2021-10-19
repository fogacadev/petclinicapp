using PetClinicApp.Source.Modules.Accounts.DTO;
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
    public class UpdatePasswordService : ServiceBase
    {
        private readonly IUsersRepository usersRepository;
        public UpdatePasswordService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task ExecuteAsync(long loggedUserId, UpdatePasswordDTO updatePassword)
        {
            ValidateModel(updatePassword);

            var user = await usersRepository.Find(loggedUserId);
            if(user == null)
            {
                throw new AppErrorException("Account does not exists", HttpStatusCode.NotFound);
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(updatePassword.Password);

            user.PasswordHash = hashedPassword;

            await usersRepository.Update(user);
        }
    }
}
