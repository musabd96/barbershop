using Domain.Models.BarberShops;
using MediatR;

namespace Application.Queries.BarberShops.GetAllBarberShops
{
    public class GetAllBarberShopsQuery : IRequest<List<BarberShop>>
    {
    }
}
