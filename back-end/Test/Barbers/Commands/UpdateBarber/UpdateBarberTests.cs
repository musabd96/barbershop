using Application.Commands.Barbers.UpdateBarber;
using Application.Dtos;
using Infrastructure.Repositories.Barbers;
using Moq;
using NUnit.Framework;

namespace Test.Barber.Commands.UpdateBarber
{
    [TestFixture]
    public class UpdateBarberTests
    {
        private Mock<IBarberRepositories> _barberRepository;
        private UpdateBarberCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _barberRepository = new Mock<IBarberRepositories>();
            _handler = new UpdateBarberCommandHandler(_barberRepository.Object);
        }

        private void SetupMockDbContext(List<Domain.Models.Barbers.Barber> barbers)
        {
            _barberRepository.Setup(repo => repo.UpdateBarber(It.IsAny<Guid>(), 
                                                              It.IsAny<string>(),
                                                              It.IsAny<string>(),
                                                              It.IsAny<string>(), 
                                                              It.IsAny<string>(), 
                                                              It.IsAny<CancellationToken>()))!
                                  .ReturnsAsync((Guid barberId, string firstName,
                                                 string lastName, string email,
                                                 string phone, CancellationToken ct) =>
                                  {
                                      var barber = barbers.FirstOrDefault(a => a.Id == barberId);
                                      if (barber != null)
                                      {
                                          barber.Id = barberId;
                                          barber.FirstName = firstName;
                                          barber.LastName = lastName;
                                          barber.Email = email;
                                          barber.Phone = phone;

                                          return barber;
                                      }
                                      return null;
                                  });
        }

        [Test]
        public async Task Handle_UpdateValidBarber_ReturnsUpdatedBarber()
        {
            // Arrange
            var barberId = Guid.NewGuid();
            var barbers = new List<Domain.Models.Barbers.Barber>
            {
                new Domain.Models.Barbers.Barber
                {
                    Id = barberId,
                    FirstName = "Test1",
                    LastName = "Test2",
                    Email = "Test1@email.com",
                    Phone = "0712345679"

                }
            };
            SetupMockDbContext(barbers);

            var updateDto = new BarberDto
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@email.com",
                Phone = "0712345678"
            };

            var command = new UpdateBarberCommand(updateDto, barberId);


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Not.Null);
            NUnit.Framework.Assert.That(result.FirstName, Is.EqualTo(updateDto.FirstName));

        }

        [Test]
        public async Task Handle_UpdateInValidBarber_ReturnsNull()
        {
            // Arrange
            var barberId = Guid.NewGuid();

            var updateDto = new BarberDto
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@email.com",
                Phone = "0712345678"
            };

            var command = new UpdateBarberCommand(updateDto, barberId);


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Null);

        }
    }
}
