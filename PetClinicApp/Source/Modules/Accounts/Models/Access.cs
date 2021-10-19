using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Accounts.Models
{
    public class Access
    {
        public Account User { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpiresOn { get; set; }
    }
}
