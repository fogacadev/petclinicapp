using PetClinicApp.Source.Modules.Accounts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Accounts.Repositories
{
    public interface IUserTokenRepository
    {
        Task<UserToken> Create(UserToken userToken);
        Task Delete(long id);
        Task<UserToken> FindByRefreshToken(string refreshToken);
        Task<UserToken> Find(long id);
    }
}
