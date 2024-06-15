﻿using Domain.Models.Barbers;
using Infrastructure.Repositories.Barbers;
using MediatR;

namespace Application.Commands.Barbers.AddNewBarber
{
    public class AddNewBarberCommandHandler : IRequestHandler<AddNewBarberCommand, Barber>
    {
        private readonly IBarberRepositories _barberRepositories;

        public AddNewBarberCommandHandler(IBarberRepositories barberRepositories)
        {
            _barberRepositories = barberRepositories;
        }

        public async Task<Barber> Handle(AddNewBarberCommand request, CancellationToken cancellationToken)
        {
            Barber newBarber = new Barber()
            {
                Id = request.NewBarber.Id,
                FirstName = request.NewBarber.FirstName,
                LastName = request.NewBarber.LastName,
                Email = request.NewBarber.Email,
                Phone = request.NewBarber.Phone,
            };

            await _barberRepositories.AddNewBarber(newBarber, cancellationToken);

            return newBarber;
        }
    }
}
