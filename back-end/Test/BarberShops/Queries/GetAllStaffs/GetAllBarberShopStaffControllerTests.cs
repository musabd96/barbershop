using API.Controllers.BarberShopController;
using Application.Queries.BarberShops.GetAllBarberShopStaff;
using Application.Validators.BarberShop;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.BarberShops.Queries.GetAllStaffs
{
    [TestFixture]
    public class GetAllBarberShopStaffControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<BarberShopValidator> _validatorMock;
        private BarberShopController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _validatorMock = new Mock<BarberShopValidator>(); // Mock the validator
            _controller = new BarberShopController(_mediatorMock.Object, _validatorMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public async Task GetAllBarberShopStaff_ShouldReturnOk()
        {
            // Arrange
            string barberShopName = "Example BarberShop";

            var expectedResult = new List<Domain.Models.Barbers.Barber>
            {
                new Domain.Models.Barbers.Barber { Id = Guid.NewGuid(), FirstName = "Barber 1" },
                new Domain.Models.Barbers.Barber { Id = Guid.NewGuid(), FirstName = "Barber 2" }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllBarberShopStaffQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetAllBarberShopStaff(barberShopName);

            // Assert
            NUnit.Framework.Assert.Multiple(() =>
            {
                NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
                NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
            });
        }
    }
}
