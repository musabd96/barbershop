

using Domain.Models.BarberShops;
using MediatR;

namespace Application.Commands.BarberShops.DeleteBarberShop
{
    public class DeleteBarberShopCommand : IRequest<BarberShop>
    {
        public DeleteBarberShopCommand(Guid barberShopId)
        {
            Id = barberShopId;
        }

        public Guid Id { get; }
    }
}
