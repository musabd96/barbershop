﻿using API.Controllers.AppointmentController;
using Application.Queries.Appointments.GetAllAppointments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.Appointment.Queries.GetAll
{
    internal class GetAllAppointmentControllerTests
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
        public async Task GetAllClassrooms_ShouldReturnOk()
        {
            // Arrange
            var expectedClassrooms = new List<Domain.Models.Appointments.Appointment>
            {
                new() {
                    Id = new Guid(),
                    BarberId = new Guid(),
                    CustomerId = new Guid(),
                    AppointmentDate = new DateOnly(),
                    Service = "Hair",
                    Price = 20.0m,
                    IsCancelled = false,
                },
                new () {
                    Id = new Guid(),
                    BarberId = new Guid(),
                    CustomerId = new Guid(),
                    AppointmentDate = new DateOnly(),
                    Service = "beard",
                    Price = 20.0m,
                    IsCancelled = false,
                }
            };

            var query = new GetAllAppointmentsQuery();

            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<GetAllAppointmentsQuery>(), default))
                .ReturnsAsync(expectedClassrooms);

            // Act
            var result = await _controller.GetAllAppointments();

            // Assert
            NUnit.Framework.Assert.Multiple(() =>
            {
                NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
                NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
            });
        }

        [Test]
        public async Task GetAllClassrooms_ShouldReturnInternalServerErrorOnException()
        {
            // Arrange
            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<GetAllAppointmentsQuery>(), default))
                .Throws(new Exception("Simulated error"));

            // Act
            var result = await _controller.GetAllAppointments();

            // Assert
            NUnit.Framework.Assert.Multiple(() =>
            {
                NUnit.Framework.Assert.That(result, Is.InstanceOf<ObjectResult>());
                NUnit.Framework.Assert.That((result as ObjectResult)?.StatusCode, Is.EqualTo(500));
            });
        }
    }
}