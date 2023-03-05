using Battleship.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IApplicationDbContext _context;
        public UserController(IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser()
        {
            var user = await _context.Users.AddAsync(new Domain.Entities.User { DateCreated = DateTime.Now, DateUpdated = DateTime.Now, Id = new Guid(), UserName = "itirsina98" });
            await _context.SaveChangesAsync(cancellationToken: default);
            return Ok(user.Entity);
        }
    }
}
