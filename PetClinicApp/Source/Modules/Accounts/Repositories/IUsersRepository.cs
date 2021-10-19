using PetClinicApp.Source.Modules.Accounts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Accounts.Repositories
{
    public interface IUsersRepository
    {

        Task<User> Find(long id);
        Task<User> Register(User user);
        Task<User> FindByEmail(string email);
        Task<User> Create(User user);
        Task Update(User user);
    }
}
