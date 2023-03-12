using Battleship.Persistence.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Battleship.Application.EventContexts.Users.Commands.AuthenticateUserCommand
{
    public class AuthenticateUserCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, string>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthenticateUserCommandHandler(IApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public Task<string> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var users = _dbContext.Users.FirstOrDefaultAsync(u => u.PasswordHash== passwordHash && u.UserName == request.UserName);

            if (users == null)
            {
                throw new Exception("Unauthorized"); // TODO : define exceptions
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: null,
                expires: DateTime.Now.AddSeconds(30),
                signingCredentials: credentials
            );

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));

        }
    }
}
