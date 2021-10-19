using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Shared.Errors
{
    public class AppError
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<string> Details { get; set; }
    }
}
