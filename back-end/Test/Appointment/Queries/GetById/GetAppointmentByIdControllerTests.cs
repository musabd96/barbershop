using API.Controllers.AppointmentController;
using Application.Queries.Appointments.GetAppointmentById;
using Application.Validators.Appointmnet;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.Appointment.Queries.GetById
{
    [TestFixture]
    public class GetAppointmentByIdControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<AppointmentValidator> _validatorMock;
        private AppointmentController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _validatorMock = new Mock<AppointmentValidator>(); // Mock the validator
            _controller = new AppointmentController(_mediatorMock.Object, _validatorMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public async Task GetAppointmentById_ShouldReturnOk_WithValidId()
        {
            // Arrange
            var expectedAppointment = new Domain.Models.Appointments.Appointment
            {
                Id = Guid.NewGuid(),
                BarberId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                AppointmentDate = DateTime.Now,
                Service = "Hair",
                Price = 20.0m,
                IsCancelled = false
            };

            var query = new GetAppointmentByIdQuery(expectedAppointment.Id);

            _mediatorMock.Setup(x => x.Send(It.Is<GetAppointmentByIdQuery>(q => q.Id == expectedAppointment.Id), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedAppointment);

            // Act
            var result = await _controller.GetAppointmentById(expectedAppointment.Id);

            // Assert
            NUnit.Framework.Assert.Multiple(() =>
            {
                NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
                NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
            });
        }

        [Test]
        public async Task GetAppointmentById_ShouldReturnInternalServerErrorOnException_WithInvalidId()
        {
            // Arrange
            var invalidAppointmentId = Guid.NewGuid();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetAppointmentByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ThrowsAsync(new Exception("Simulated database error"));

            // Act
            var result = await _controller.GetAppointmentById(invalidAppointmentId);

            // Assert
            NUnit.Framework.Assert.Multiple(() =>
            {
                NUnit.Framework.Assert.That(result, Is.InstanceOf<ObjectResult>());
                NUnit.Framework.Assert.That((result as ObjectResult)?.StatusCode, Is.EqualTo(500));
                NUnit.Framework.Assert.That((result as ObjectResult)?.Value, Is.EqualTo("Simulated database error"));
            });
        }
    }
}
