
using Application.Queries.Customers.GetCustomerById;
using Infrastructure.Repositories.Customers;
using Moq;
using NUnit.Framework;

namespace Test.Customers.Queries.GetById
{
    [TestFixture]
    public class GetCustomerByIdTests
    {
        private GetCustomerByIdQueryHandler _handler;
        private Mock<ICustomerRepositories> _customerRepositories;

        [SetUp]
        public void Setup()
        {
            _customerRepositories = new Mock<ICustomerRepositories>();
            _handler = new GetCustomerByIdQueryHandler(_customerRepositories.Object);
        }

        protected void SetupMockDbContext(Domain.Models.Customers.Customer customer)
        {
            _customerRepositories.Setup(repo => repo.GetCustomerById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(customer);
        }

        [Test]
        public async Task Handle_ValidId_ReturnsCorrectCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var expectedCustomer = new Domain.Models.Customers.Customer
            {
                Id = customerId,
                FirstName = "Foo",
                LastName = "Bar",
                Email = "foo.Bar@email.com",
                Phone = "0712345678"
            };

            SetupMockDbContext(expectedCustomer);

            var query = new GetCustomerByIdQuery(customerId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result.Id, Is.EqualTo(expectedCustomer.Id));
        }

        [Test]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            var query = new GetCustomerByIdQuery(customerId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Null);
        }
    }
}
