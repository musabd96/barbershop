using API.Controllers.AppointmentController;
using Application.Commands.Appointments.DeleteAppointment;
using Application.Dtos;
using Application.Validators.Appointmnet;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.Appointment.Commands.DeleteAppointment
{
    [TestFixture]
    public class DeleteAppoinmentControllerTests
    {
        private IMediator _mediator;
        private AppointmentValidator _validator;
        private AppointmentController _controller;

        [SetUp]
        public void Setup()
        {
            _mediator = Mock.Of<IMediator>();
            _validator = Mock.Of<AppointmentValidator>(); // Mock the validator
            _controller = new AppointmentController(_mediator, _validator);
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


            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<DeleteAppointmentCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.Models.Appointments.Appointment());

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
            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<DeleteAppointmentCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Models.Appointments.Appointment)null!);

            // Act
            var result = await _controller.DeleteAppointment(appointmentId);

            // Assert
            var errors = (result as BadRequestObjectResult)?.Value as SerializableError;
            NUnit.Framework.Assert.That(errors!.ContainsKey("AppointmentNotFound"));
        }
    }
}
