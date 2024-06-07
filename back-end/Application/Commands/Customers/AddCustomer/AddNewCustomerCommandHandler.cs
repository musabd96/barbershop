using Domain.Models.Customers;
using Infrastructure.Repositories.Customers;
using MediatR;

namespace Application.Commands.Customers.AddCustomer
{
    public class AddNewCustomerCommandHandler : IRequestHandler<AddNewCustomerCommand, Customer>
    {
        private readonly ICustomerRepositories _customerRepositories;

        public AddNewCustomerCommandHandler(ICustomerRepositories customerRepositories)
        {
            _customerRepositories = customerRepositories;
        }
        public async Task<Customer> Handle(AddNewCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer newCustomer = new Customer()
            {
                Id = request.NewCustomer.Id,
                FirstName = request.NewCustomer.FirstName,
                LastName = request.NewCustomer.LastName,
                Email = request.NewCustomer.Email,
                Phone = request.NewCustomer.Phone,
            };

            await _customerRepositories.AddNewCustomer(newCustomer, cancellationToken);

            return newCustomer;
        }
    }
}
