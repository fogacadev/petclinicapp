using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Accounts.Entities
{
    public class UserToken
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
