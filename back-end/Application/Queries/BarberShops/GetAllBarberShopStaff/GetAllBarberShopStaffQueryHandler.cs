using Domain.Models.Barbers;
using Infrastructure.Repositories.BarberShops;
using MediatR;

namespace Application.Queries.BarberShops.GetAllBarberShopStaff
{
    internal class GetAllBarberShopStaffQueryHandler : IRequestHandler<GetAllBarberShopStaffQuery, List<Barber>>
    {
        private readonly IBarberShopRepositories _barberShopRepositories;

        public GetAllBarberShopStaffQueryHandler(IBarberShopRepositories barberShopRepositories)
        {
            _barberShopRepositories = barberShopRepositories;
        }

        public async Task<List<Barber>> Handle(GetAllBarberShopStaffQuery request, CancellationToken cancellationToken)
        {
            List<Barber> AllBarberShops = await _barberShopRepositories.GetAllBarberShopStaff(request.BarberShopName, cancellationToken);
            return AllBarberShops;
        }
    }
}
