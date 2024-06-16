using Domain.Models.BarberShops;
using Infrastructure.Repositories.BarberShops;
using MediatR;

namespace Application.Queries.BarberShops.GetAllBarberShops
{
    public class GetAllBarberShopsQueryHandler : IRequestHandler<GetAllBarberShopsQuery, List<BarberShop>>
    {
        private readonly IBarberShopRepositories _barberShopRepositories;

        public GetAllBarberShopsQueryHandler(IBarberShopRepositories barberShopRepositories)
        {
            _barberShopRepositories = barberShopRepositories;
        }

        public async Task<List<BarberShop>> Handle(GetAllBarberShopsQuery request, CancellationToken cancellationToken)
        {
            List<BarberShop> AllBarberShops = await _barberShopRepositories.GetAllBarberShops(cancellationToken);
            return AllBarberShops;
        }
    }
}
