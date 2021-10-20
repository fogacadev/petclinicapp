using PetClinicApp.Source.Modules.Accounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Accounts.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime BirthDate { get; set; }
        public string Avatar { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<UserToken> Tokens { get; set; }
    }

    public static class UserExtensios
    {
        public static Account ToAccount(this User user)
        {
            return new Account
            {
                Id = user.Id,
                BirthDate = user.BirthDate,
                CreatedAt = user.CreatedAt,
                Email = user.Email,
                FullName = user.FullName
            };
        }

        public static User ToEntitiy(this Account account)
        {
            return new User
            {
                Id = account.Id,
                BirthDate = account.BirthDate,
                CreatedAt = account.CreatedAt,
                Email = account.Email,
                FullName = account.FullName
            };
        }
    }
}
