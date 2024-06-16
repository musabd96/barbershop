using Domain.Models.BarberShops;
using Infrastructure.Repositories.BarberShops;
using MediatR;

namespace Application.Commands.BarberShops.DeleteBarberShop
{
    public class DeleteBarberShopCommandHandler : IRequestHandler<DeleteBarberShopCommand, BarberShop>
    {
        private readonly IBarberShopRepositories _barberShopRepositories;

        public DeleteBarberShopCommandHandler(IBarberShopRepositories barberShopRepositories)
        {
            _barberShopRepositories = barberShopRepositories;
        }

        public async Task<BarberShop> Handle(DeleteBarberShopCommand request, CancellationToken cancellationToken)
        {
            var barberShop = await _barberShopRepositories.DeleteBarberShop(request.Id, cancellationToken);

            return barberShop;
        }
    }
}
