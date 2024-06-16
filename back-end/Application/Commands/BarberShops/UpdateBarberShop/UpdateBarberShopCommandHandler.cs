
using Domain.Models.BarberShops;
using Infrastructure.Repositories.BarberShops;
using MediatR;

namespace Application.Commands.BarberShops.UpdateBarberShop
{
    public class UpdateBarberShopCommandHandler : IRequestHandler<UpdateBarberShopCommand, BarberShop>
    {
        private readonly IBarberShopRepositories _barberShopRepositories;

        public UpdateBarberShopCommandHandler(IBarberShopRepositories barberShopRepositories)
        {
            _barberShopRepositories = barberShopRepositories;
        }

        public async Task<BarberShop> Handle(UpdateBarberShopCommand request, CancellationToken cancellationToken)
        {
            var barberShopToUpdate = await _barberShopRepositories.UpdateBarberShop(request.BarberShopId, request.BarberShopDto.Name,
                                                                                    request.BarberShopDto.Email, request.BarberShopDto.Phone,
                                                                                    request.BarberShopDto.Address, request.BarberShopDto.ZipCode,
                                                                                    request.BarberShopDto.City, cancellationToken);

            return barberShopToUpdate;
        }
    }
}
