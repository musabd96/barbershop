using Domain.Models.Barbers;
using MediatR;

namespace Application.Queries.Barbers.GetBarberById
{
    public class GetBarberByIdQuery : IRequest<Barber>
    {
        public GetBarberByIdQuery(Guid barbertId)
        {
            Id = barbertId;
        }

        public Guid Id { get; set; }
    }
}
