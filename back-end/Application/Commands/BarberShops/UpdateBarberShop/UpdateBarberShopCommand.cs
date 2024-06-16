using Application.Dtos;
using Domain.Models.BarberShops;
using MediatR;

namespace Application.Commands.BarberShops.UpdateBarberShop
{
    public class UpdateBarberShopCommand : IRequest<BarberShop>
    {
        public UpdateBarberShopCommand(BarberShopDto barberShopDto, Guid barberShopId)
        {
            BarberShopDto = barberShopDto;
            BarberShopId = barberShopId;
        }

        public BarberShopDto BarberShopDto { get; set; }
        public Guid BarberShopId { get; set; }
    }
}
