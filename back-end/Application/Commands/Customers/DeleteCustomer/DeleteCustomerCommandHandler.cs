using Domain.Models.Customers;
using Infrastructure.Repositories.Customers;
using MediatR;

namespace Application.Commands.Customers.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Customer>
    {
        private readonly ICustomerRepositories _customerRepositories;

        public DeleteCustomerCommandHandler(ICustomerRepositories customerRepositories)
        {
            _customerRepositories = customerRepositories;
        }
        public async Task<Customer> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepositories.DeleteCustomer(request.Id, cancellationToken);

            return customer;
        }
    }
}
