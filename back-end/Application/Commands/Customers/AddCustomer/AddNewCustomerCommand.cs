using Application.Dtos;
using Domain.Models.Customers;
using MediatR;

namespace Application.Commands.Customers.AddCustomer
{
    public class AddNewCustomerCommand : IRequest<Customer>
    {
        public AddNewCustomerCommand(CustomerDto newCustomer)
        {
            NewCustomer = newCustomer;
        }

        public CustomerDto NewCustomer { get; }
    }
}
