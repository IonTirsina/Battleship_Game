using MediatR;
using Battleship.Domain.Entities;
using Battleship.Persistence.Interfaces;

namespace Battleship.Application.EventContexts.Users.Commands.CreateUserCommand
{
    public class CreateUserCommand : IRequest<User>
    {
        public string UserName { get; set; } = "";
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
            // TODO : automapper;
            User userToCreate = new User(command.UserName);

            var createdUser = await _dbContext.Users.AddAsync(userToCreate, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return createdUser.Entity;
        }
    }
}
