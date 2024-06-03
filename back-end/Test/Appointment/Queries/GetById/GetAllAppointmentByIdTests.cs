
using API.Controllers.AppointmentController;
using Application.Queries.Appointments.GetAllAppointments;
using Application.Queries.Appointments.GetAppointmentById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.Appointment.Queries.GetById
{
    internal class GetAllAppointmentByIdTests
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
        public async Task GetAppointmentById_ShouldReturnOk()
        {
            var appointmentId = Guid.NewGuid();
            var expectedAppointment = new Domain.Models.Appointments.Appointment();

            Mock.Get(_mediator)
               .Setup(mediator => mediator.Send(It.IsAny<GetAppointmentByIdQuery>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(expectedAppointment);

            // Act
            var result = await _controller.GetAppointmentById(appointmentId);

            // Assert
            NUnit.Framework.Assert.Multiple(() =>
            {
                NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
                NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
            });
        }

        [Test]
        public async Task GetAppointmentById_ShouldReturnInternalServerErrorOnException()
        {
            // Arrange
            var appointmentId = new Guid();

            Mock.Get(_mediator)
                .Setup(mediator => mediator.Send(It.IsAny<GetAppointmentByIdQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Simulated exception"));

            // Act
            var result = await _controller.GetAppointmentById(appointmentId);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<ObjectResult>());
            var objectResult = (ObjectResult)result;
            NUnit.Framework.Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        }
    }
}
