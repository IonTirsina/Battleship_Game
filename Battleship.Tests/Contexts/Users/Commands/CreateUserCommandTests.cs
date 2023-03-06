using Battleship.Application.EventContexts.Users.Commands.CreateUserCommand;
using Battleship.Domain.Entities;
using Battleship.Persistence.Interfaces;
using Battleship.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace Battleship.Tests.Contexts.Users.Commands
{

    [TestClass]
    public class CreateUserCommandTests
    {
        private CreateUserCommandHandler _commandHandler;
        public CreateUserCommandTests()
        {
            _commandHandler = new CreateUserCommandHandler(TestApplicationDbContext.Get());
        }

        [TestMethod]
        public async Task CreateUser_ShouldCreate()
        {
            CreateUserCommand command = new CreateUserCommand()
            {
                Password = "Test123!",
                PasswordConfirmation = "Test123!",
                UserName = "Test CreateUser_ShouldCreate"
            };

            User createdUser = await _commandHandler.Handle(command, new CancellationToken());

            Assert.IsNotNull(createdUser);
        }
   
        [TestMethod]

        public async Task CreateUser_PasswordConfirmation()
        {
            CreateUserCommand command = new CreateUserCommand()
            {
                Password = "test123",
                PasswordConfirmation = "test",
                UserName = "Test CreateUser_PasswordConfirmation"
            };

            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _commandHandler.Handle(command, new CancellationToken()));
        }

        [TestMethod]
        public async Task CreateUser_PasswordEncrypted()
        {
            var testPassword = "Test123!";
            CreateUserCommand command = new CreateUserCommand()
            {
                Password = testPassword,
                PasswordConfirmation = testPassword,
                UserName = "Test CreateUser_PasswordEncrypted"
            };

            User createdUser = await _commandHandler.Handle(command, new CancellationToken());

            var passwordMatchesHash = BCrypt.Net.BCrypt.Verify(testPassword, createdUser.PasswordHash);

            Assert.IsTrue(passwordMatchesHash);
        }
    }
}
