using Application.Commands.Appointments.AddNewAppoinment;
using Application.Dtos;
using Infrastructure.Repositories.Appointments;
using Moq;
using NUnit.Framework;

namespace Test.Appointment.Commands.Appointment
{
    [TestFixture]
    public class AddNewAppoinmentTests
    {
        private AddNewAppointmentCommandHandler _handler;
        private Mock<IAppointmentRepositories> _appointmentRepositories;

        [SetUp]
        public void Setup()
        {
            _appointmentRepositories = new Mock<IAppointmentRepositories>();
            _handler = new AddNewAppointmentCommandHandler(_appointmentRepositories.Object);
        }

        protected void SetupMockDbContext(List<Domain.Models.Appointments.Appointment> appointments)
        {
            _appointmentRepositories.Setup(repo => repo.AddNewAppoinment(It.IsAny<Domain.Models.Appointments.Appointment>(), It.IsAny<CancellationToken>()))
                .Callback((Domain.Models.Appointments.Appointment appointment,
                    CancellationToken cancellationToken) => appointments.Add(appointment))
                .Returns((Domain.Models.Appointments.Appointment appointment,
                    CancellationToken cancellationToken) => Task.FromResult(appointment));
        }

        [Test]
        public async Task Handle_ValidAppointment_ReturnsNewAppointment()
        {
            // Arrange
            var appointment = new List<Domain.Models.Appointments.Appointment>();
            SetupMockDbContext(appointment);

            var newAppointment = new AppointmentDto
            {
                CustomerId = Guid.NewGuid(),
                BarberId = Guid.NewGuid(),
                AppointmentDate = DateTime.Now,
                Service = "Hair cut",
                Price = 299.99m,
                IsCancelled = false,
            };


            var addAppointmentCommand = new AddNewAppointmentCommand(newAppointment);

            // Act
            var result = await _handler!.Handle(addAppointmentCommand, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result.Id, Is.EqualTo(newAppointment.Id));
        }

        //[Test]
        //public async Task Handle_inValidAppointment_ShouldReturnInternalServerErrorOnException()
        //{
        //    // Arrange
        //    var appointment = new List<Domain.Models.Appointments.Appointment>();
        //    SetupMockDbContext(appointment);

        //    var newAppointment = new AppointmentDto
        //    {
        //        CustomerId = Guid.Empty,
        //        BarberId = Guid.Empty,
        //        AppointmentDate = DateTime.Now,
        //        Service = "",
        //        Price = -1,
        //    };


        //    var addAppointmentCommand = new AddNewAppointmentCommand(newAppointment);

        //    // Act
        //    var result = await _handler!.Handle(addAppointmentCommand, CancellationToken.None);

        //    // Assert
        //}
    }
}
