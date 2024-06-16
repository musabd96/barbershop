using Application.Dtos;
using Domain.Models.BarberShops;
using MediatR;

namespace Application.Commands.BarberShops.AddNewBarberShop
{
    public class AddNewBarberShopCommand : IRequest<BarberShop>
    {
        public AddNewBarberShopCommand(BarberShopDto newBarberShop)
        {
            NewBarberShop = newBarberShop;
        }

        public BarberShopDto NewBarberShop { get; set; }
    }
}
