

using Application.Dtos;
using Application.Queries.Users.Login;
using Domain.Models.Users;
using Infrastructure.Repositories.Authorization;
using Moq;
using NUnit.Framework;

namespace Test.Authentication.Queries
{
    [TestFixture]
    public class LoginTest
    {
        private LoginUserQueryHandler _handler;
        private Mock<IAuthRepository> _authRepository;

        [SetUp]
        public void Setup()
        {
            _authRepository = new Mock<IAuthRepository>();
            _handler = new LoginUserQueryHandler(_authRepository.Object);
        }


        private void SetupMockUserRepository(List<User> users)
        {
            _authRepository.Setup(repo => repo.AuthenticateUser(It.IsAny<string>(),
                                                    It.IsAny<string>(),
                                                    It.IsAny<CancellationToken>()))
              .Returns((string username, string password, CancellationToken cancellationToken) =>
              {
                  return users.FirstOrDefault(u => u.Username == username &&
                  BCrypt.Net.BCrypt.Verify(password, u.PasswordHash))!;
              });


            _authRepository.Setup(repo => repo.GenerateJwtToken(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                          .Returns("fakeJwtToken");
        }

        [Test]
        public async Task Handle_ValidLogin_ReturnsToken()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Username = "testing", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Testing123!") }
            };

            SetupMockUserRepository(users);

            var loginUserQuery = new LoginUserQuery(new UserDto
            {
                Username = "testing",
                Password = "Testing123!"
            });

            // Act
            var result = await _handler.Handle(loginUserQuery, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.EqualTo("fakeJwtToken"));
        }

        [Test]
        public async Task Handle_InvalidLogin_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Username = "testing", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Testing123!") }
            };

            SetupMockUserRepository(users);

            var loginUserQuery = new LoginUserQuery(new UserDto
            {
                Username = "fake",
                Password = "Testing123!"
            });

            // Act and Assert
            var ex = NUnit.Framework.Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            {
                await _handler.Handle(loginUserQuery, CancellationToken.None);
            });

            // Assert
            NUnit.Framework.Assert.That(ex!.Message, Is.EqualTo("Invalid username or password."));
        }


    }
}
