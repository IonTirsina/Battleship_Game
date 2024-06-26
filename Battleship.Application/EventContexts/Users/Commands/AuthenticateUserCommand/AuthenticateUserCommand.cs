﻿using Battleship.Persistence.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Battleship.Application.EventContexts.Users.Commands.AuthenticateUserCommand
{
    public class AuthenticateUserCommand : IRequest<string?>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, string?>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthenticateUserCommandHandler(IApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public async Task<string?> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName && u.PasswordHash == request.Password);

            if (users == null)
            {
                return null;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            double sessionLifetime = _configuration.GetValue<double>("Jwt:SessionLifetime");

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: null,
                expires: DateTime.Now.AddSeconds(sessionLifetime),
                signingCredentials: credentials
            );

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));

        }
    }
}
