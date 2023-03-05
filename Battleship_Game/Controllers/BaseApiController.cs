using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class BaseApiController : Controller
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
