using Application.Dtos;
using FluentValidation;

namespace Application.Validators.BarberShop
{
    public class BarberShopValidator : AbstractValidator<BarberShopDto>
    {
        public BarberShopValidator()
        {
            RuleFor(barber => barber.Id)
                .NotEmpty().WithMessage("barber Id is required.");

            RuleFor(barber => barber.Name)
                .NotEmpty().WithMessage("barbershop's name is required.");

            RuleFor(barber => barber.Email)
                .NotEmpty().WithMessage("barber's email is required.");

            RuleFor(barber => barber.Phone)
                .NotEmpty().WithMessage("barber's name is required.");

            RuleFor(barber => barber.Address)
                .NotEmpty().WithMessage("barber's address is required.");

            RuleFor(barber => barber.ZipCode)
                .NotEmpty().WithMessage("barber's zipCode is required.");

            RuleFor(barber => barber.City)
                .NotEmpty().WithMessage("barber's city is required.");
        }
    }
}
