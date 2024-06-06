using API.Controllers.AppointmentController;
using Application.Dtos;
using Application.Validators.Appointmnet;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.Appointment.Commands.UpdateAppointment
{
    [TestFixture]
    public class UpdateAppointmentControllerTests
    {
        private AppointmentController _controller;
        private AppointmentValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new AppointmentValidator();
            _controller = new AppointmentController(Mock.Of<IMediator>(), _validator);
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
        public async Task UpdateAppointment_ValidId_ReturnsOkResult()
        {
            // Arrange
            var appointmentId = Guid.NewGuid();
            var appointmentDto = new AppointmentDto
            {
                Id = appointmentId,
                CustomerId = Guid.NewGuid(),
                BarberId = Guid.NewGuid(),
                AppointmentDate = DateTime.Now.AddDays(1),
                Service = "Haircut",
                Price = 399.00M,
                IsCancelled = false
            };


            // Act
            var result = await _controller.UpdateAppointment(appointmentDto, appointmentId);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
            NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task UpdateAppointment_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var appointmentId = Guid.Empty;
            var appointmentDto = new AppointmentDto
            {
                CustomerId = Guid.NewGuid(),
                BarberId = Guid.NewGuid(),
                AppointmentDate = DateTime.Now.AddDays(1),
                Service = "Haircut",
                Price = 399.00M,
                IsCancelled = false
            };

            // Act
            var result = await _controller.UpdateAppointment(appointmentDto, appointmentId);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            NUnit.Framework.Assert.That((result as BadRequestObjectResult)?.StatusCode, Is.EqualTo(400));
        }
    }
}
