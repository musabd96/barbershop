
using Application.Queries.Customers.GetAllCustomers;
using Infrastructure.Repositories.Customers;
using Moq;
using NUnit.Framework;

namespace Test.Customers.Queries.GetAll
{
    [TestFixture]
    public class GetAllCustomersTests
    {
        private GetAllCustomersQueryHandler _handler;
        private GetAllCustomersQuery _request;
        private Mock<ICustomerRepositories> _customerRepositories;

        [SetUp]
        public void Setup()
        {
            _customerRepositories = new Mock<ICustomerRepositories>();
            _handler = new GetAllCustomersQueryHandler(_customerRepositories.Object);
            _request = new GetAllCustomersQuery();
        }

        protected void SetupMockDbContext(List<Domain.Models.Customers.Customer> customers)
        {
            _customerRepositories.Setup(repo => repo.GetAllCustomers(It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(customers);
        }

        [Test]
        public async Task Handle_ValidRequest_ReturnsListOfCustomers()
        {
            // Arrange
            var customersList = new List<Domain.Models.Customers.Customer>
            {
                new Domain.Models.Customers.Customer
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Foo",
                    LastName = "Bar",
                    Email = "foo.Bar@email.com",
                    Phone = "0712345678"
                },
                new Domain.Models.Customers.Customer
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Foo",
                    LastName = "Bar",
                    Email = "foo.Bar@email.com",
                    Phone = "0712345678"
                },
            };

            SetupMockDbContext(customersList);

            // Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result.Count, Is.EqualTo(customersList.Count));
        }

        [Test]
        public async Task Handle_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyCustomersList = new List<Domain.Models.Customers.Customer>();
            SetupMockDbContext(emptyCustomersList);

            // Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Empty);
        }
    }
}
