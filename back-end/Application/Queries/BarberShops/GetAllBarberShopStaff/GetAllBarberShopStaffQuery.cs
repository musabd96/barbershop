using Domain.Models.Barbers;
using MediatR;

namespace Application.Queries.BarberShops.GetAllBarberShopStaff
{
    public class GetAllBarberShopStaffQuery : IRequest<List<Barber>>
    {
        public GetAllBarberShopStaffQuery(string barberShopName)
        {
            BarberShopName = barberShopName;
        }

        public string BarberShopName { get; }
    }
}
