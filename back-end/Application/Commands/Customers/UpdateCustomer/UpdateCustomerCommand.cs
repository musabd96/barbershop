using Application.Dtos;
using Domain.Models.Customers;
using MediatR;

namespace Application.Commands.Customers.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<Customer>
    {
        public UpdateCustomerCommand(CustomerDto customerDto, Guid customerId)
        {
            CustomerDto = customerDto;
            CustomerId = customerId;
        }

        public CustomerDto CustomerDto { get; set; }
        public Guid CustomerId { get; set; }
    }
}
