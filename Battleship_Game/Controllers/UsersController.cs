using Battleship.Application.EventContexts.Users.Commands.AuthenticateUserCommand;
using Battleship.Application.EventContexts.Users.Commands.CreateUserCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Battleship.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([Required][FromBody] CreateUserCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([Required] AuthenticateUserCommand request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}
