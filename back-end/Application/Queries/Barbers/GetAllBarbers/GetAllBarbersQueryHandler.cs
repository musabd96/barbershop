using Domain.Models.Barbers;
using Infrastructure.Repositories.Barbers;
using MediatR;

namespace Application.Queries.Barbers.GetAllBarbers
{
    public class GetAllBarbersQueryHandler : IRequestHandler<GetAllBarbersQuery, List<Barber>>
    {
        private readonly IBarberRepositories _barberRepositories;

        public GetAllBarbersQueryHandler(IBarberRepositories barberRepositories)
        {
            _barberRepositories = barberRepositories;
        }

        public async Task<List<Barber>> Handle(GetAllBarbersQuery request, CancellationToken cancellationToken)
        {
            List<Barber> AllBarbers = await _barberRepositories.GetAllBarbers(cancellationToken);
            return AllBarbers;
        }
    }
}
