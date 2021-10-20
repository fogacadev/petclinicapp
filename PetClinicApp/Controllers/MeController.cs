using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetClinicApp.Source.Modules.Accounts.Models;
using PetClinicApp.Source.Modules.Accounts.Services;
using PetClinicApp.Source.Shared.Jwt;
using System.Threading.Tasks;

namespace PetClinicApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MeController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Account>> Get([FromServices] FindLoggedUserService findLoggedUserService)
        {
            var loggedUserId = User.GetUserId();

            var account = await findLoggedUserService.ExecuteAsync(loggedUserId);

            return Ok(account);
        }
    }
}
