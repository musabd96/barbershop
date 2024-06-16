using Domain.Models.Barbers;
using Domain.Models.BarberShops;
using Infrastructure.Repositories.BarberShops;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.BarberShops.GetBarberShopById
{
    public class GetBarberShopByIdQueryHandler : IRequestHandler<GetBarberShopByIdQuery, BarberShop>
    {
        private readonly IBarberShopRepositories _barberShopRepositories;

        public GetBarberShopByIdQueryHandler(IBarberShopRepositories barberShopRepositories)
        {
            _barberShopRepositories = barberShopRepositories;
        }

        public async Task<BarberShop> Handle(GetBarberShopByIdQuery request, CancellationToken cancellationToken)
        {
            BarberShop barberShop = await _barberShopRepositories.GetBarberShopById(request.Id, cancellationToken);

            return barberShop;
        }
    }
}
