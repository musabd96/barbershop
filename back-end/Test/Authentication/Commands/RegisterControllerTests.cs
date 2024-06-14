using API.Controllers.AuthenticationController;
using Application.Commands.Users.Register;
using Application.Dtos;
using Application.Validators.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.Authentication.Commands
{
    [TestFixture]
    public class RegisterControllerTests
    {
        private AuthenticationController _controller;
        private UserValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new UserValidator(); // Use real validator instance
            _controller = new AuthenticationController(Mock.Of<IMediator>(), _validator);
        }

        [TearDown]
        public void TearDown()
        {
            if (_controller != null)
            {
                _controller.Dispose();
            }
        }

        [Test]
        public async Task Register_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var user = new UserDto
            {
                Username = "test",
                Password = "Test123!"
            };

            var customer = new CustomerDto
            {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                Email = "test@test.com",
                Phone = "0712345678"
            };

            var command = new RegisterUserCommand(user, customer);

            // Act
            var result = await _controller.Register(command);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
            NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task Register_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            var user = new UserDto
            {
                Username = "test",
                Password = "Test12!" // Invalid password input need 8 characters
            };

            var customer = new CustomerDto
            {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                Email = "test@test.com",
                Phone = "0712345678"
            };

            var command = new RegisterUserCommand(user, customer);

            // Act
            var result = await _controller.Register(command);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            NUnit.Framework.Assert.That((result as BadRequestObjectResult)?.StatusCode, Is.EqualTo(400));
        }
    }
}
