using Application.Dtos;
using Domain.Models.Barbers;
using MediatR;

namespace Application.Commands.Barbers.UpdateBarber
{
    public class UpdateBarberCommand : IRequest<Barber>
    {
        public UpdateBarberCommand(BarberDto barberDto, Guid barberId)
        {
            BarberDto = barberDto;
            BarberId = barberId;
        }

        public BarberDto BarberDto { get; set; }
        public Guid BarberId { get; set; }
    }
}
