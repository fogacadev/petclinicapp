using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetClinicApp.Source.Modules.Accounts.Models;
using PetClinicApp.Source.Modules.Accounts.Services;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Jwt;
using System.Threading.Tasks;

namespace PetClinicApp.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class MeController : ControllerBase
    {

        /// <summary>
        /// Retorna o usuário logado
        /// </summary>
        /// <param name="findLoggedUserService"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Account),200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
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
