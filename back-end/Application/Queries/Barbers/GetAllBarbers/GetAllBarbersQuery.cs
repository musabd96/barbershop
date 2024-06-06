using Domain.Models.Barbers;
using MediatR;

namespace Application.Queries.Barbers.GetAllBarbers
{
    public class GetAllBarbersQuery : IRequest<List<Barber>>
    {
    }
}
