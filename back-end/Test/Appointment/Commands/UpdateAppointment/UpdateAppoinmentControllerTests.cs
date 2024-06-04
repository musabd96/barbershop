using API.Controllers.AppointmentController;
using Application.Commands.Appointments.AddNewAppoinment;
using Application.Commands.Appointments.UpdateAppointment;
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

namespace Test.Appointment.Commands.UpdateAppointment
{
    [TestFixture]
    public class UpdateAppoinmentControllerTests
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
        public async Task UpdateAppoinment_ValidId_ReturnsOkResult()
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
                .Setup(x => x.Send(It.IsAny<UpdateAppointmentCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.Models.Appointments.Appointment());

            // Act
            var result = await _controller.UpdateAppointment(appointmentDto, appointmentId);


            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
            NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task UpdateAppoinment_inValidId_ReturnsBadRequest()
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
                .Setup(x => x.Send(It.IsAny<UpdateAppointmentCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Models.Appointments.Appointment)null!);

            // Act
            var result = await _controller.UpdateAppointment(appointmentDto, appointmentId);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<NotFoundResult>());
            var notFoundResult = result as NotFoundResult;
            NUnit.Framework.Assert.That(notFoundResult?.StatusCode, Is.EqualTo(404));
        }


    }
}
