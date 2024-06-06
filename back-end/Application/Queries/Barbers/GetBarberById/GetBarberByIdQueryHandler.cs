using Domain.Models.Barbers;
using Infrastructure.Repositories.Barbers;
using MediatR;

namespace Application.Queries.Barbers.GetBarberById
{
    public class GetBarberByIdQueryHandler : IRequestHandler<GetBarberByIdQuery, Barber>
    {
        private readonly IBarberRepositories _barberRepositories;

        public GetBarberByIdQueryHandler(IBarberRepositories barberRepositories)
        {
            _barberRepositories = barberRepositories;
        }

        public async Task<Barber> Handle(GetBarberByIdQuery request, CancellationToken cancellationToken)
        {
            Barber barber = await _barberRepositories.GetBarberById(request.Id, cancellationToken);

            return barber;
        }
    }
}
