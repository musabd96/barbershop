using Application.Dtos;
using Domain.Models.Customers;
using Domain.Models.Users;
using MediatR;

namespace Application.Commands.Users.Register
{
    public class RegisterUserCommand : IRequest<User>
    {
        public RegisterUserCommand(UserDto newUser, CustomerDto newCustomer)
        {
            NewUser = newUser;
            NewCustomer = newCustomer;
        }

        public UserDto NewUser { get; }
        public CustomerDto NewCustomer { get; }
    }
}
