using Domain.Models.Customers;
using MediatR;

namespace Application.Commands.Customers.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<Customer>
    {
        public DeleteCustomerCommand(Guid customerId)
        {
            Id = customerId;
        }

        public Guid Id { get; }
    }
}
