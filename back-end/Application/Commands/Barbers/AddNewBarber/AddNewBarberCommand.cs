using Application.Dtos;
using Domain.Models.Barbers;
using MediatR;

namespace Application.Commands.Barbers.AddNewBarber
{
    public class AddNewBarberCommand : IRequest<Barber>
    {
        public AddNewBarberCommand(UserDto newUser, BarberDto newBarber, string barbershopName)
        {
            NewUser = newUser;
            NewBarber = newBarber;
            BarbershopName = barbershopName;
        }

        public UserDto NewUser { get; set; }
        public BarberDto NewBarber { get; set; }
        public string BarbershopName { get; set; }
    }
}
