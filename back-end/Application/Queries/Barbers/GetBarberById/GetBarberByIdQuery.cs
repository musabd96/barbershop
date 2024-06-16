using Domain.Models.Barbers;
using MediatR;

namespace Application.Queries.Barbers.GetBarberById
{
    public class GetBarberByIdQuery : IRequest<Barber>
    {
        public GetBarberByIdQuery(Guid barberId)
        {
            Id = barberId;
        }

        public Guid Id { get; set; }
    }
}
