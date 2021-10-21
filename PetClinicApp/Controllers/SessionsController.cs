using Microsoft.AspNetCore.Mvc;
using PetClinicApp.Source.Modules.Accounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PetClinicApp.Source.Modules.Accounts.Services;
using PetClinicApp.Source.Modules.Accounts.DTO;
using Microsoft.AspNetCore.Authorization;

namespace PetClinicApp.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SessionsController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("create-session")]
        public async Task<ActionResult<Access>> CreateSessions([FromServices] CreateSessionService createSessionService, [FromBody]Login login)
        {
            var access = await createSessionService.ExecuteAsync(login);

            return Ok(access);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<Access>> RefreshToken([FromServices] RefreshTokenService refreshTokenService, [FromQuery] string refreshToken)
        {
            var access = await refreshTokenService.ExecuteAsync(refreshToken);

            return Ok(access);
        }
    }
}
