using Application.Dtos;
using Domain.Models.Barbers;
using MediatR;

namespace Application.Commands.Barbers.AddNewBarber
{
    public class AddNewBarberCommand : IRequest<Barber>
    {
        public AddNewBarberCommand(UserDto newUser, BarberDto newBarber)
        {
            NewUser = newUser;
            NewBarber = newBarber;
        }

        public UserDto NewUser { get; set; }
        public BarberDto NewBarber { get; set; }
    }
}
