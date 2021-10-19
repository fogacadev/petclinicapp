using PetClinicApp.Source.Modules.Accounts.Entities;
using PetClinicApp.Source.Modules.Accounts.Models;
using PetClinicApp.Source.Modules.Accounts.Repositories;
using PetClinicApp.Source.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Accounts.Services
{
    public class CreateSessionService
    {
        private readonly IUsersRepository usersRepository;
        private readonly IUserTokenRepository userTokenRepository;
        private readonly GenerateRandomTokenService generateRandomTokenService;
        private readonly GenerateTokenService generateTokenService;

        public CreateSessionService(IUsersRepository usersRepository, 
            IUserTokenRepository userTokenRepository,
            GenerateRandomTokenService generateRandomTokenService,
            GenerateTokenService generateTokenService)
        {
            this.usersRepository = usersRepository;
            this.userTokenRepository = userTokenRepository;
            this.generateRandomTokenService = generateRandomTokenService;
            this.generateTokenService = generateTokenService;
        }

        public async Task<Access> ExecuteAsync(Login login)
        {

            var user = await usersRepository.FindByEmail(login.Email);

            if(user == null)
            {
                throw new AppErrorException("Incorrect username or password", HttpStatusCode.NotFound);
            }

            if(BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash) == false)
            {
                throw new AppErrorException("Incorrect username or password", HttpStatusCode.NotFound);
            }

            var token = generateTokenService.ExecuteAsync(user);
            var refreshToken = generateRandomTokenService.Execute();
            var tokenExpiresOn = DateTime.Now.AddDays(10);

            var userToken = new UserToken();
            userToken.Token = refreshToken;
            userToken.UserId = user.Id;
            userToken.ExpiresOn = tokenExpiresOn;

            await userTokenRepository.Create(userToken);
            

            var access = new Access();
            access.Token = token;
            access.RefreshToken = refreshToken;
            access.TokenExpiresOn = tokenExpiresOn;
            access.User = user.ToAccount();

            return access;
        }
    }
}
