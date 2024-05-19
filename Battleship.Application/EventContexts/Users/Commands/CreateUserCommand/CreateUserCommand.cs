using MediatR;
using Battleship.Domain.Entities;
using Battleship.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Battleship.Application.EventContexts.Users.Commands.CreateUserCommand
{
    public class CreateUserCommand : IRequest<User>
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public string PasswordConfirmation { get; set; } = "";
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateUserCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            if (command.Password != command.PasswordConfirmation)
            {
                throw new ArgumentException("Password confirmation does not match password");
            }

            bool userExists = await _dbContext.Users.AnyAsync(u => u.UserName == command.UserName);

            if (userExists)
            {
                throw new DuplicateNameException("User with such username already exists");
            }

            User userToCreate = new(command.UserName, command.Password);

            var createdUser = await _dbContext.Users.AddAsync(userToCreate, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return createdUser.Entity;
        }
    }
}
