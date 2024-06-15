using NUnit.Framework;
using API.Controllers.AppointmentController;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Application.Commands.Appointments.AddNewAppoinment;
using Application.Validators.Appointmnet;

namespace YourProject.Tests.Controllers
{
    [TestFixture]
    public class AppointmentControllerTests
    {
        private AppointmentController _controller;
        private AppointmentValidator _validator;
        private Mock<IMediator> _mediatorMock;
        private Mock<HttpContext> _httpContextMock;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _validator = new AppointmentValidator(); // Use real validator instance

            _httpContextMock = new Mock<HttpContext>();
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "testuser")
            });
            var claimsPrincipal = new ClaimsPrincipal(identity);
            _httpContextMock.Setup(x => x.User).Returns(claimsPrincipal);

            _controller = new AppointmentController(_mediatorMock.Object, _validator)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContextMock.Object
                }
            };
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
        public async Task AddNewAppointment_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var appointmentDto = new AppointmentDto
            {
                Id = Guid.NewGuid(),
                BarberId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Service = "Hair cut",
                AppointmentDate = new DateTime(2030, 01, 01),
                Price = 299.99m,
                IsCancelled = false,
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<AddNewAppointmentCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(new Domain.Models.Appointments.Appointment
                         {
                             Id = appointmentDto.Id
                         });

            // Act
            var result = await _controller.AddNewAppointment(appointmentDto);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
            NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task AddNewAppointment_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            var appointmentDto = new AppointmentDto
            {
                // Example of an invalid appointmentDto
                Id = Guid.Empty, // Assume Id is required
                Service = "", // Assume Service is required
                AppointmentDate = DateTime.MinValue, // Assume valid future date is required
                Price = -1 // Assume price must be a positive value
            };

            // Act
            var result = await _controller.AddNewAppointment(appointmentDto);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            NUnit.Framework.Assert.That((result as BadRequestObjectResult)?.StatusCode, Is.EqualTo(400));
        }
    }
}
