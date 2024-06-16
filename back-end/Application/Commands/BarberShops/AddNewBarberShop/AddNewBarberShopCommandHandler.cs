using Domain.Models.BarberShops;
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

            // Get all existing barber shops
            List<BarberShop> AllBarberShops = await _barberShopRepositories.GetAllBarberShops(cancellationToken);

            // Check if any existing barber shop already has the same name
            if (AllBarberShops.Any(bs => bs.Name == request.NewBarberShop.Name))
            {
                throw new Exception("Barber shop with the same name already exists.");
            }

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
