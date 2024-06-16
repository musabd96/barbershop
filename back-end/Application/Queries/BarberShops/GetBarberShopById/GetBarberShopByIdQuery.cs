
using Domain.Models.BarberShops;
using MediatR;

namespace Application.Queries.BarberShops.GetBarberShopById
{
    public class GetBarberShopByIdQuery : IRequest<BarberShop>
    {
        public GetBarberShopByIdQuery(Guid barberShopId)
        {
            Id = barberShopId;
        }

        public Guid Id { get; set; }
    }
}
