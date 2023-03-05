using Battleship.Application.EventContexts.Users.Commands.CreateUserCommand;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Battleship.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> RegisterUser([Required][FromBody] CreateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
