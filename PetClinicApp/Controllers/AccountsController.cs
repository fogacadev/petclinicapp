using Microsoft.AspNetCore.Mvc;
using PetClinicApp.Source.Modules.Accounts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PetClinicApp.Source.Modules.Accounts.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using PetClinicApp.Source.Modules.Accounts.Models;
using PetClinicApp.Source.Shared.Jwt;
using Microsoft.AspNetCore.Http;

namespace PetClinicApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountsController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromServices] RegisterUserService registerUserService, [FromBody] RegisterUserDTO registerUser)
        {
            await registerUserService.ExecuteAsync(registerUser);

            return NoContent();
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Update([FromServices] UpdateUserService updateUserService, [FromBody] UpdateAccountDTO account)
        {
            long loggedUserId = User.GetUserId();

            await updateUserService.ExecuteAsync(account, loggedUserId);

            return NoContent();
        }

        [Authorize]
        [HttpPut("update-password")]
        public async Task<ActionResult> UpdatePassword([FromServices] UpdatePasswordService updatePasswordService,[FromBody] UpdatePasswordDTO updatePassword)
        {
            long loggedUserId = User.GetUserId();

            await updatePasswordService.ExecuteAsync(loggedUserId, updatePassword);

            return NoContent();
        }

        [Authorize]
        [HttpPost("avatar")]
        public async Task<ActionResult> UploadAvatar([FromServices] UploadAccountAvatarService uploadAccountAvatarService, [FromForm] IFormFile file)
        {
            long loggedUserId = User.GetUserId();

            await uploadAccountAvatarService.ExecuteAsync(loggedUserId, file);

            return NoContent();
        }

        [Authorize]
        [HttpGet("avatar")]
        public async Task<ActionResult> DownloadAvatar([FromServices] DownloadAccountAvatarService downloadAccountAvatarService)
        {
            long loggedUserId = User.GetUserId();

            var fileModel = await downloadAccountAvatarService.ExecuteAsync(loggedUserId);

            return File(fileModel.Buffer, fileModel.MimeType);
        }
    }
}
