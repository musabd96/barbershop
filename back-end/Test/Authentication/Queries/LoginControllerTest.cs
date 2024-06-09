using API.Controllers.AuthenticationController;
using Application.Dtos.Users;
using Application.Dtos;
using Application.Queries.Appointments.GetAllAppointments;
using Application.Queries.Users.Login;
using Application.Validators.User;
using Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.Authentication.Queries
{
    [TestFixture]
    public class LoginControllerTest
    {
        private IMediator _mediator;
        private AuthenticationController _controller;
        private UserValidator _validator;

        [SetUp]
        public void Setup()
        {
            _mediator = Mock.Of<IMediator>();
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
        public async Task Login_ValidInput_ReturnsToken()
        {
            // Arrange
            var userDto = new UserDto
            {
                Username = "Test",
                Password = "Test123!"
            };

            // Setup mediator to return a token
            Mock.Get(_mediator)
                .Setup(m => m.Send(It.IsAny<LoginUserQuery>(), default))
                .ReturnsAsync("dummyToken");

            // Act
            var result = await _controller.Login(userDto);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
            NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task Login_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            var invalidUserDto = new UserDto
            {
                Username = "Test",
                Password = "Test123"
            };

            // Act
            var result = await _controller.Login(invalidUserDto);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<ObjectResult>());
            NUnit.Framework.Assert.That((result as ObjectResult)?.StatusCode, Is.EqualTo(400));
        }
    }
}
