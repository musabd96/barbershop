﻿using Domain.Models.Appointments;
using Infrastructure.Repositories.Appointments;
using Moq;
using NUnit.Framework; // Make sure you have the correct using statement for NUnit
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Appointments.GetAllAppointments
{
    [TestFixture]
    public class GetAllAppointmentsTests
    {
        private GetAllAppointmentsQueryHandler _handler;
        private GetAllAppointmentsQuery _request;
        private Mock<IAppointmentRepositories> _appointmentRepositories;

        [SetUp]
        public void Setup()
        {
            _appointmentRepositories = new Mock<IAppointmentRepositories>();
            _handler = new GetAllAppointmentsQueryHandler(_appointmentRepositories.Object);
            _request = new GetAllAppointmentsQuery(); // Ensure this matches the correct GetAllAppointmentsQuery class
        }

        protected void SetupMockDbContext(List<Appointment> appointments)
        {
            _appointmentRepositories.Setup(repo => repo.GetAllAppointments(It.IsAny<CancellationToken>()))
                .ReturnsAsync(appointments);
        }

        [Test]
        public async Task Handle_ValidRequest_ReturnsListOfAppointments()
        {
            // Arrange
            var appointmentsList = new List<Appointment>
            {
                new Appointment { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid(), BarberId = Guid.NewGuid(), AppointmentDate = new DateOnly(), Service = "Cutting", Price = 20.00m, IsCancelled = false },
                new Appointment { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid(), BarberId = Guid.NewGuid(), AppointmentDate = new DateOnly(), Service = "Cutting", Price = 20.00m, IsCancelled = false },
            };

            SetupMockDbContext(appointmentsList);

            // Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result.Count, Is.EqualTo(appointmentsList.Count)); 
        }

        [Test]
        public async Task Handle_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyappointmentsList = new List<Appointment>();
            SetupMockDbContext(emptyappointmentsList);

            // Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Empty);
        }
    }
}