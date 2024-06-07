using Application.Commands.Customers.DeleteCustomer;
using Infrastructure.Repositories.Customers;
using Moq;
using NUnit.Framework;

namespace Test.Customers.Commands.DeleteCustomer
{
    [TestFixture]
    public class DeleteCustomerTests
    {
        private DeleteCustomerCommandHandler _handler;
        private Mock<ICustomerRepositories> _customerRepositories;

        [SetUp]
        public void Setup()
        {
            _customerRepositories = new Mock<ICustomerRepositories>();
            _handler = new DeleteCustomerCommandHandler(_customerRepositories.Object);
        }

        protected void SetupMockDbContext(List<Domain.Models.Customers.Customer> customers)
        {
            _customerRepositories.Setup(repo => repo.DeleteCustomer(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns((Guid customerId, CancellationToken cancellationToken) =>
                {
                    var customerToDelete = customers.FirstOrDefault(customer => customer.Id == customerId);
                    if (customerToDelete != null)
                    {
                        customers.Remove(customerToDelete);
                        return Task.FromResult(customerToDelete);
                    }
                    return Task.FromResult<Domain.Models.Customers.Customer>(null);
                });
        }

        [Test]
        public async Task Handle_ValidId_DeleteCustomer()
        {
            // Arrange
            var customerId = new Guid("12345678-1234-5678-1234-567812345678");
            var customer = new List<Domain.Models.Customers.Customer>
            {
                new Domain.Models.Customers.Customer
                {
                    Id = customerId,

                }
            };
            SetupMockDbContext(customer);

            var command = new DeleteCustomerCommand(customerId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Not.Null);
            NUnit.Framework.Assert.That(result.Id, Is.EqualTo(customerId));
        }


        [Test]
        public async Task Handle_inValidId_DeleteCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            var command = new DeleteCustomerCommand(customerId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Null);
        }
    }
}
