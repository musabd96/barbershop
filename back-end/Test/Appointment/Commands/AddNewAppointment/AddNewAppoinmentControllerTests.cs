using API.Controllers.AppointmentController;
using Application.Commands.Appointments.AddNewAppoinment;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Appointment.Commands.Appointment
{
    [TestFixture]
    public class AddNewAppoinmentControllerTests
    {
        private IMediator _mediator;
        private AppointmentController _controller;

        [SetUp]
        public void Setup()
        {
            _mediator = Mock.Of<IMediator>();
            _controller = new AppointmentController(_mediator);
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
            var newAppointment = new AppointmentDto
            {
                Id = Guid.NewGuid(),
            };


            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<AddNewAppointmentCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.Models.Appointments.Appointment());

            // Act
            var result = await _controller.AddNewAppointment(newAppointment);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
            NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task AddNewAppointment_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            var invalidAppointment = new AppointmentDto
            {
                // Example of invalid input for demonstration
                // Omitting required fields or setting invalid values
            };

            _controller.ModelState.AddModelError("PropertyName", "ErrorMessage"); // Add model state error as needed

            // Act
            var result = await _controller.AddNewAppointment(invalidAppointment);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            NUnit.Framework.Assert.That((result as BadRequestObjectResult)?.StatusCode, Is.EqualTo(400));
        }
    }
}
