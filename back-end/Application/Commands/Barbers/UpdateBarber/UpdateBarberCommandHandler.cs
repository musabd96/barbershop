using Domain.Models.Barbers;
using Infrastructure.Repositories.Barbers;
using MediatR;

namespace Application.Commands.Barbers.UpdateBarber
{
    public class UpdateBarberCommandHandler : IRequestHandler<UpdateBarberCommand, Barber>
    {
        private readonly IBarberRepositories _barberRepositories;

        public UpdateBarberCommandHandler(IBarberRepositories barberRepositories)
        {
            _barberRepositories = barberRepositories;
        }

        public async Task<Barber> Handle(UpdateBarberCommand request, CancellationToken cancellationToken)
        {
            var barberToUpdate = await _barberRepositories.UpdateBarber(request.BarberId, request.BarberDto.Name, cancellationToken);

            return barberToUpdate;
        }
    }
}
