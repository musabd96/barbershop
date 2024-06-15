using API.Controllers.AppointmentController;
using Application.Commands.Appointments.DeleteAppointment;
using Application.Commands.Appointments.UpdateAppointment;
using Application.Dtos;
using Application.Validators.Appointmnet;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Security.Claims;

namespace Test.Appointment.Commands.DeleteAppointment
{
    [TestFixture]
    public class DeleteAppoinmentControllerTests
    {
        private AppointmentController _controller;
        private AppointmentValidator _validator;
        private Mock<IMediator> _mediatorMock;
        private Mock<HttpContext> _httpContextMock;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _validator = new AppointmentValidator();

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
        public async Task DeleteAppoinment_ValidId_ReturnsOkResult()
        {
            // Arrange
            var appointmentId = Guid.NewGuid();
            var appointmentDto = new AppointmentDto
            {
                CustomerId = Guid.NewGuid(),
                BarberId = Guid.NewGuid(),
                AppointmentDate = DateTime.Now.AddDays(1),
                Service = "Haircut",
                Price = 399.00M,
                IsCancelled = false
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteAppointmentCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(new Domain.Models.Appointments.Appointment
                         {
                             Id = appointmentId
                         });


            // Act
            var result = await _controller.DeleteAppointment(appointmentId);


            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
            NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task DeleteAppoinment_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var appointmentId = Guid.NewGuid();

            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteAppointmentCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((Domain.Models.Appointments.Appointment)null!);


            // Act
            var result = await _controller.DeleteAppointment(appointmentId);

            // Assert
            var errors = (result as BadRequestObjectResult)?.Value as SerializableError;
            NUnit.Framework.Assert.That(errors!.ContainsKey("AppointmentNotFound"));
        }
    }
}
