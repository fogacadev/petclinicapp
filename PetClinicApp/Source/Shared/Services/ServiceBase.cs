using PetClinicApp.Source.Shared.Errors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Shared.Services
{
    public class ServiceBase
    {
        protected void ValidateModel(object obj)
        {
            var context = new ValidationContext(obj, null, null);

            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(obj, context, results, true))
            {
                throw new AppErrorException("Houve erros de validação", results.Select(r => r.ErrorMessage).ToList() ,HttpStatusCode.BadRequest);
            }
        }

        protected bool ModelIsValid(object obj)
        {
            var context = new ValidationContext(obj, null, null);

            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(obj, context, results, true))
            {
                return false;
            }

            return true;
        }
    }
}
