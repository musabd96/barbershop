using Domain.Models.Barbers;
using MediatR;

namespace Application.Commands.Barbers.DeleteBarber
{
    public class DeleteBarberCommand : IRequest<Barber>
    {
        public DeleteBarberCommand(Guid barberId)
        {
            Id = barberId;
        }

        public Guid Id { get; }
    }
}
