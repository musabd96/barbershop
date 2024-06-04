using Application.Queries.Appointments.GetAppointmentById;
using Infrastructure.Repositories.Appointments;
using Moq;
using NUnit.Framework;

namespace Test.Appointment.Queries.GetById
{
    [TestFixture]
    public class GetAppointmentByIdTests
    {
        private GetAppointmentByIdQueryHandler _handler;
        private Mock<IAppointmentRepositories> _appointmentRepositories;

        [SetUp]
        public void Setup()
        {
            _appointmentRepositories = new Mock<IAppointmentRepositories>();
            _handler = new GetAppointmentByIdQueryHandler(_appointmentRepositories.Object);
        }

        protected void SetupMockDbContext(Domain.Models.Appointments.Appointment appointment)
        {
            _appointmentRepositories.Setup(repo => repo.GetAppointmentById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(appointment);
        }

        [Test]
        public async Task Handle_ValidId_ReturnsCorrectAppointment()
        {
            // Arrange
            var appointmentId = Guid.NewGuid();
            var expectedAppointment = new Domain.Models.Appointments.Appointment
            {
                Id = appointmentId,
                CustomerId = Guid.NewGuid(),
                BarberId = Guid.NewGuid(),
                AppointmentDate = DateTime.Now.AddDays(1),
                Service = "Haircut",
                Price = 399.00M,
                IsCancelled = false
            };

            SetupMockDbContext(expectedAppointment);

            var query = new GetAppointmentByIdQuery(appointmentId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result.Id, Is.EqualTo(expectedAppointment.Id));
        }

        [Test]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var appointmentId = Guid.NewGuid();

            var query = new GetAppointmentByIdQuery(appointmentId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Null);
        }
    }
}
