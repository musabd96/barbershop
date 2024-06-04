using Application.Commands.Appointments.UpdateAppointment;
using Application.Dtos;
using Infrastructure.Repositories.Appointments;
using Moq;
using NUnit.Framework;

namespace Test.Appointment.Commands.UpdateAppointment
{
    [TestFixture]
    public class UpdateAppointmentTests
    {
        private Mock<IAppointmentRepositories> _appointmentRepository;
        private UpdateAppointmentCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _appointmentRepository = new Mock<IAppointmentRepositories>();
            _handler = new UpdateAppointmentCommandHandler(_appointmentRepository.Object);
        }

        private void SetupMockDbContext(List<Domain.Models.Appointments.Appointment> appointments)
        {
            _appointmentRepository.Setup(repo => repo.UpdateAppointment(It.IsAny<Guid>(),
                                                                        It.IsAny<Guid>(),
                                                                        It.IsAny<Guid>(),
                                                                        It.IsAny<DateTime>(),
                                                                        It.IsAny<string>(),
                                                                        It.IsAny<decimal>(),
                                                                        It.IsAny<bool>(),
                                                                        It.IsAny<CancellationToken>()))!
                                  .ReturnsAsync((Guid appointmentId,
                                                 Guid customerId,
                                                 Guid barberId,
                                                 DateTime date,
                                                 string service,
                                                 decimal price,
                                                 bool isCancelled,
                                                 CancellationToken ct) =>
            {
                var appointment = appointments.FirstOrDefault(a => a.Id == appointmentId);
                if (appointment != null)
                {
                    appointment.CustomerId = customerId;
                    appointment.BarberId = barberId;
                    appointment.AppointmentDate = date;
                    appointment.Service = service;
                    appointment.Price = price;
                    appointment.IsCancelled = isCancelled;
                    return appointment;
                }
                return null;
            });
        }

        [Test]
        public async Task Handle_UpdateValidAppointment_ReturnsUpdatedAppointment()
        {
            // Arrange
            var appointmentId = Guid.NewGuid();
            var appointments = new List<Domain.Models.Appointments.Appointment>
            {
                new Domain.Models.Appointments.Appointment
                {
                    Id = appointmentId,
                    CustomerId = Guid.NewGuid(),
                    BarberId = Guid.NewGuid(),
                    AppointmentDate = DateTime.Now.AddDays(1),
                    Service = "Haircut",
                    Price = 25.00m,
                    IsCancelled = false
                }
            };
            SetupMockDbContext(appointments);

            var updateDto = new AppointmentDto
            {
                AppointmentDate = new DateTime(2020, 06, 10),
                Service = "Shave",
                Price = 199.99m,
            };

            var command = new UpdateAppointmentCommand(updateDto, appointmentId);


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Not.Null);
            NUnit.Framework.Assert.That(result.AppointmentDate, Is.EqualTo(updateDto.AppointmentDate));
            NUnit.Framework.Assert.That(result.Service, Is.EqualTo(updateDto.Service));
            NUnit.Framework.Assert.That(result.Price, Is.EqualTo(updateDto.Price));

        }

        [Test]
        public async Task Handle_UpdateInValidAppointment_ReturnsNull()
        {
            // Arrange
            var appointmentId = Guid.NewGuid();

            var updateDto = new AppointmentDto
            {
                AppointmentDate = new DateTime(2020, 06, 10),
                Service = "Shave",
                Price = 199.99m,
            };

            var command = new UpdateAppointmentCommand(updateDto, appointmentId);


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Null);

        }



    }
}
