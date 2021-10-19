using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Accounts.Services
{
    public class GenerateRandomTokenService
    {

        public string Execute()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");

            return GuidString;
        }
    }
}
