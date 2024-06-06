using Application.Dtos;
using Domain.Models.Barbers;
using MediatR;

namespace Application.Commands.Barbers.AddNewBarber
{
    public class AddNewBarberCommand : IRequest<Barber>
    {
        public AddNewBarberCommand(BarberDto newBarber)
        {
            NewBarber = newBarber;
        }

        public BarberDto NewBarber { get; }
    }
}
