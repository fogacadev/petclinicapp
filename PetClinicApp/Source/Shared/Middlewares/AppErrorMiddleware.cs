using Microsoft.AspNetCore.Http;
using PetClinicApp.Source.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Shared.Middlewares
{

    public class AppErrorMiddleware
    {
        private readonly RequestDelegate next;
        public AppErrorMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (AppErrorException ex)
            {
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = (int)ex.StatusCode;
                var result = JsonSerializer.Serialize(new AppError
                {
                    Code = ex.GetHashCode().ToString(),
                    Message = ex.Message,
                    Details = ex.Details
                });

                await context.Response.WriteAsync(result);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var result = JsonSerializer.Serialize(new AppError
                {
                    Code = ex.GetHashCode().ToString(),
                    Message = ex.Message,
                    Details = new List<string>()
                });

                await context.Response.WriteAsync(result);
            }

        }
    }
}
