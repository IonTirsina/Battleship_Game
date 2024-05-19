using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Api.Controllers
{
    [ApiController()]
    [Route("api/[Controller]")]
    public class HealthCheckController : BaseApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult CheckHealth()
        {
            return Ok("Working");
        }
    }
}
