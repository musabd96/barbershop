using NUnit.Framework;
using API.Controllers.AppointmentController;
using Application.Dtos;
using Application.Validators.Appointmnet;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace YourProject.Tests.Controllers
{
    [TestFixture]
    public class AppointmentControllerTests
    {
        private AppointmentController _controller;
        private AppointmentValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new AppointmentValidator(); // Use real validator instance
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
        public async Task AddNewAppoinment_ValidInput_ReturnsOkResult()
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
            };

            // Act
            var result = await _controller.AddNewAppointment(appointmentDto);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            NUnit.Framework.Assert.That((result as BadRequestObjectResult)?.StatusCode, Is.EqualTo(400));
        }

    }
}
