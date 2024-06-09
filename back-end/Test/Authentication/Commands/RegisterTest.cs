using Application.Commands.Users.Register;
using Application.Dtos;
using Domain.Models.Users;
using Infrastructure.Repositories.Users;
using Moq;
using NUnit.Framework;

namespace Test.Authentication.Commands
{
    [TestFixture]
    public class RegisterTest
    {
        private RegisterUserCommandHandler _handler;
        private Mock<IUserRepository> _userRepository;
        private RegisterUserCommandValidator _validator;

        [SetUp]
        public void Setup()
        {
            _userRepository = new Mock<IUserRepository>();
            _validator = new RegisterUserCommandValidator(_userRepository.Object);
            _handler = new RegisterUserCommandHandler(_userRepository.Object, _validator);
        }

        protected void SetupMockDbContext(List<User> users)
        {
            _userRepository.Setup(repo => repo.GetAllUsers())
                   .ReturnsAsync(users);

            _userRepository.Setup(repo => repo.RegisterUser(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Callback((User user, CancellationToken cancellationToken) => users.Add(user))
                .Returns((User user, CancellationToken cancellationToken) => Task.FromResult(user));
        }

        [Test]
        public async Task RegisterUser_ValidCommand_Successful()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Username = "Test1", PasswordHash = "Test111!!" },
                new User { Id = Guid.NewGuid(), Username = "Test2", PasswordHash = "Test222!!" }
            };
            SetupMockDbContext(users);

            var command = new UserDto
            {
                Username = "testuser",
                Password = "Password123!",
            };

            var registerCommand = new RegisterUserCommand(command);

            // Act
            var result = await _handler.Handle(registerCommand, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result.Username, Is.EqualTo(command.Username));
        }
        [Test]
        public async Task RegisterUser_InvalidCommand_ThrowsArgumentException()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Username = "Test1", PasswordHash = "Test111!!" },
                new User { Id = Guid.NewGuid(), Username = "Test2", PasswordHash = "Test222!!" }
            };
            SetupMockDbContext(users);

            var command = new UserDto
            {
                Username = "Test1",
                Password = "Test111!!"
            };

            var registerCommand = new RegisterUserCommand(command);

            // Act and Assert
            var ex = NUnit.Framework.Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _handler.Handle(registerCommand, CancellationToken.None);
            });

            // Assert
            NUnit.Framework.Assert.That(ex!.Message, Is.EqualTo("Registration error: Username is already taken."));
        }


    }
}
