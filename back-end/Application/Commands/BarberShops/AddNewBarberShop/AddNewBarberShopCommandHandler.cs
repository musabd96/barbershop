using Domain.Models.Barbers;
using Domain.Models.BarberShops;
using Domain.Models.Users;
using Infrastructure.Repositories.BarberShops;
using MediatR;

namespace Application.Commands.BarberShops.AddNewBarberShop
{
    public class AddNewBarberShopCommandHandler : IRequestHandler<AddNewBarberShopCommand, BarberShop>
    {
        private readonly IBarberShopRepositories _barberShopRepositories;

        public AddNewBarberShopCommandHandler(IBarberShopRepositories barberShopRepositories)
        {
            _barberShopRepositories = barberShopRepositories;
        }

        public async Task<BarberShop> Handle(AddNewBarberShopCommand request, CancellationToken cancellationToken)
        {
            BarberShop newBarberShop = new BarberShop()
            {
                Id = Guid.NewGuid(),
                Name = request.NewBarberShop.Name,
                Email = request.NewBarberShop.Email,
                Phone = request.NewBarberShop.Phone,
                Address = request.NewBarberShop.Address,
                ZipCode = request.NewBarberShop.ZipCode,
                City = request.NewBarberShop.City,
            };

            await _barberShopRepositories.AddNewBarberShop(newBarberShop, cancellationToken);

            return newBarberShop;
        }
    }
}
