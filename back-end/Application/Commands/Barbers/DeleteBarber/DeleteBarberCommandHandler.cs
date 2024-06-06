
using Application.Commands.Appointments.DeleteAppointment;
using Domain.Models.Appointments;
using Domain.Models.Barbers;
using Infrastructure.Repositories.Barbers;
using MediatR;

namespace Application.Commands.Barbers.DeleteBarber
{
    public class DeleteBarberCommandHandler : IRequestHandler<DeleteBarberCommand, Barber>
    {
        private readonly IBarberRepositories _barberRepositories;

        public DeleteBarberCommandHandler(IBarberRepositories barberRepositories)
        {
            _barberRepositories = barberRepositories;
        }
        public async Task<Barber> Handle(DeleteBarberCommand request, CancellationToken cancellationToken)
        {
            var barber = await _barberRepositories.DeleteBarber(request.Id, cancellationToken);

            return barber;
        }
    }
}
