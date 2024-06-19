using API.Controllers.AppointmentController;
using Application.Queries.Appointments.GetAllAppointments;
using Application.Validators.Appointmnet;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Security.Claims;

namespace Test.Appointment.Queries.GetAll
{
    [TestFixture]
    public class GetAllAppointmentControllerTests
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

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
           {
                new Claim(ClaimTypes.Name, "test")
           }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
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
        public async Task GetAllAppointments_ShouldReturnOk()
        {
            // Arrange
            var expectedAppointments = new List<Domain.Models.Appointments.Appointment>
            {
                new() {
                    Id = new Guid(),
                    BarberId = new Guid(),
                    CustomerId = new Guid(),
                    AppointmentDate = new DateTime(),
                    Service = "Hair",
                    Price = 20.0m,
                    IsCanceled = false,
                },
                new () {
                    Id = new Guid(),
                    BarberId = new Guid(),
                    CustomerId = new Guid(),
                    AppointmentDate = new DateTime(),
                    Service = "beard",
                    Price = 20.0m,
                    IsCanceled = false,
                }
            };
            var username = "test";
            var query = new GetAllAppointmentsQuery(username);

            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<GetAllAppointmentsQuery>(), default))
                .ReturnsAsync(expectedAppointments);

            // Act
            var result = await _controller.GetAllAppointments();

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
            NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GetAllAppointments_ShouldReturnInternalServerErrorOnException()
        {
            // Arrange
            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<GetAllAppointmentsQuery>(), default))
                .Throws(new Exception("Simulated error"));

            // Act
            var result = await _controller.GetAllAppointments();

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<ObjectResult>());
            NUnit.Framework.Assert.That((result as ObjectResult)?.StatusCode, Is.EqualTo(500));
        }
    }
}
