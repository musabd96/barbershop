using Application.Commands.Appointments.DeleteAppointment;
using Infrastructure.Repositories.Appointments;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Test.Appointment.Commands.DeleteAppointment
{
    [TestFixture]
    public class DeleteAppointmentTests
    {
        private DeleteAppointmentCommandHandler _handler;
        private Mock<IAppointmentRepositories> _appointmentRepositories;

        [SetUp]
        public void Setup()
        {
            _appointmentRepositories = new Mock<IAppointmentRepositories>();
            _handler = new DeleteAppointmentCommandHandler(_appointmentRepositories.Object);
        }

        protected void SetupMockDbContext(List<Domain.Models.Appointments.Appointment> appointments)
        {
            _appointmentRepositories.Setup(repo => repo.DeleteAppointment(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid appointmentId, string username, CancellationToken cancellationToken) =>
                {
                    var appointmentToDelete = appointments.FirstOrDefault(appointment => appointment.Id == appointmentId);
                    if (appointmentToDelete != null)
                    {
                        appointments.Remove(appointmentToDelete);
                        return appointmentToDelete;
                    }
                    return null;
                });
        }

        [Test]
        public async Task Handle_ValidId_DeleteAppointment()
        {
            // Arrange
            var appointmentId = new Guid("12345678-1234-5678-1234-567812345678");
            var appointment = new List<Domain.Models.Appointments.Appointment>
            {
                new Domain.Models.Appointments.Appointment
                {
                    Id = appointmentId
                }
            };
            SetupMockDbContext(appointment);

            var username = "test";
            var command = new DeleteAppointmentCommand(appointmentId, username);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Not.Null);
            NUnit.Framework.Assert.That(result.Id, Is.EqualTo(appointmentId));
        }

        [Test]
        public async Task Handle_inValidId_DeleteAppointment()
        {
            // Arrange
            var appointmentId = Guid.NewGuid();
            var appointment = new List<Domain.Models.Appointments.Appointment>(); // Empty list for invalid id test
            SetupMockDbContext(appointment);

            var username = "test";
            var command = new DeleteAppointmentCommand(appointmentId, username);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Null);
        }
    }
}
