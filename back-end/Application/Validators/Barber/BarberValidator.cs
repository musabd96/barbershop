using Application.Dtos;
using FluentValidation;

namespace Application.Validators.Barber
{
    public class BarberValidator : AbstractValidator<BarberDto>
    {
        public BarberValidator()
        {
            RuleFor(barber => barber.Id)
                .NotEmpty().WithMessage("barber Id is required.");

            RuleFor(barber => barber.FirstName)
                .NotEmpty().WithMessage("barber's name is required.");

            RuleFor(barber => barber.LastName)
                .NotEmpty().WithMessage("barber's name is required.");

            RuleFor(barber => barber.Email)
                .NotEmpty().WithMessage("barber's name is required.");
            RuleFor(barber => barber.Phone)
                .NotEmpty().WithMessage("barber's name is required.");
        }
    }
}
