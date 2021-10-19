using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Shared.Errors
{
    public class AppErrorException : Exception
    {
        public HttpStatusCode StatusCode  { get; }
        public List<string> Details { get; set; }
        public AppErrorException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
        {
            this.Details = new List<string>();
            this.StatusCode = statusCode;
        }

        public AppErrorException(string message, List<string> details, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
        {
            this.Details = details;
            this.StatusCode = statusCode;
        }
    }
}
