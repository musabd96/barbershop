using Application.Commands.Customers.AddCustomer;
using Application.Dtos;
using Infrastructure.Repositories.Customers;
using Moq;
using NUnit.Framework;

namespace Test.Customers.Commands.AddNewCustomer
{
    [TestFixture]
    public class AddNewCustomerTests
    {
        private AddNewCustomerCommandHandler _handler;
        private Mock<ICustomerRepositories> _customerRepositories;

        [SetUp]
        public void Setup()
        {
            _customerRepositories = new Mock<ICustomerRepositories>();
            _handler = new AddNewCustomerCommandHandler(_customerRepositories.Object);
        }

        protected void SetupMockDbContext(List<Domain.Models.Customers.Customer> customers)
        {
            _customerRepositories.Setup(repo => repo.AddNewCustomer(It.IsAny<Domain.Models.Customers.Customer>(), It.IsAny<CancellationToken>()))
                .Callback((Domain.Models.Customers.Customer customer,
                    CancellationToken cancellationToken) => customers.Add(customer))
                .Returns((Domain.Models.Customers.Customer customer,
                    CancellationToken cancellationToken) => Task.FromResult(customer));
        }

        [Test]
        public async Task Handle_Validcustomer_ReturnsNewcustomer()
        {
            // Arrange
            var customer = new List<Domain.Models.Customers.Customer>();
            SetupMockDbContext(customer);

            var newcustomer = new CustomerDto
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@email.com",
                Phone = "0712345678"
            };


            var addcustomerCommand = new AddNewCustomerCommand(newcustomer);

            // Act
            var result = await _handler!.Handle(addcustomerCommand, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result.Id, Is.EqualTo(newcustomer.Id));
        }
    }
}
