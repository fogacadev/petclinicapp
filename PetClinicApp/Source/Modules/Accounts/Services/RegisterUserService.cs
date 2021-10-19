using PetClinicApp.Source.Modules.Accounts.DTO;
using PetClinicApp.Source.Modules.Accounts.Entities;
using PetClinicApp.Source.Modules.Accounts.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Accounts.Services
{
    public class RegisterUserService : ServiceBase
    {
        private readonly IUsersRepository usersRepository;
        public RegisterUserService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task ExecuteAsync(RegisterUserDTO registerUser)
        {

            var userAlreadyExists = await usersRepository.FindByEmail(registerUser.Email);

            if (userAlreadyExists != null)
            {
                throw new AppErrorException("User already exists");
            }

            ValidateModel(registerUser);


            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerUser.Password);

            var user = new User
            {
                Id = 0,
                BirthDate = registerUser.BirthDate,
                Email = registerUser.Email,
                CreatedAt = DateTime.Now,
                FullName = registerUser.FullName,
                PasswordHash = hashedPassword,
            };

            var createdUser = await usersRepository.Create(user);

            createdUser.ToAccount();
        }
    }
}
