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
    public class RefreshTokenService
    {
        private IUserTokenRepository userTokenRepository;
        private readonly IUsersRepository usersRepository;
        private readonly GenerateTokenService generateTokenService;
        private readonly GenerateRandomTokenService generateRandomTokenService;
        public RefreshTokenService(IUserTokenRepository userTokenRepository,
            IUsersRepository usersRepository,
            GenerateTokenService generateTokenService,
            GenerateRandomTokenService generateRandomTokenService)
        {
            this.userTokenRepository = userTokenRepository;
            this.usersRepository = usersRepository;
            this.generateTokenService = generateTokenService;
            this.generateRandomTokenService = generateRandomTokenService;
        }

        public async Task<Access> ExecuteAsync(string receivedRefreshToken)
        {
            var lastUserToken = await userTokenRepository.FindByRefreshToken(receivedRefreshToken);
            if(lastUserToken == null)
            {
                throw new AppErrorException("Token inválido", HttpStatusCode.BadRequest);
            }

            var user = await usersRepository.Find(lastUserToken.UserId);
            if(user == null)
            {
                throw new AppErrorException("Token inválido", HttpStatusCode.BadRequest);
            }

            if(DateTime.Now >= lastUserToken.ExpiresOn)
            {
                throw new AppErrorException("Token inválido", HttpStatusCode.BadRequest);
            }

            

            var token = generateTokenService.ExecuteAsync(user);
            var refreshToken = generateRandomTokenService.Execute();
            var tokenExpiresOn = DateTime.Now.AddDays(10);

            var userToken = new UserToken();
            userToken.Token = refreshToken;
            userToken.UserId = user.Id;
            userToken.ExpiresOn = tokenExpiresOn;

            await userTokenRepository.Create(userToken);
            await userTokenRepository.Delete(lastUserToken.Id);

            var access = new Access();
            access.Token = token;
            access.RefreshToken = refreshToken;
            access.TokenExpiresOn = tokenExpiresOn;
            access.User = user.ToAccount();

            return access;

        }
    }
}
