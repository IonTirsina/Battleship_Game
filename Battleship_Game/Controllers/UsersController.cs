using Battleship.Application.EventContexts.Users.Commands.AuthenticateUserCommand;
using Battleship.Application.EventContexts.Users.Commands.CreateUserCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace Battleship.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        [HttpPost]
        [AllowAnonymous]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        ///<summary>
        /// Create a user
        /// </summary>
        public async Task<IActionResult> RegisterUser([Required][FromBody] CreateUserCommand request)
        {
            await Mediator.Send(request);
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> LoginUser([Required] AuthenticateUserCommand request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}
