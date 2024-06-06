using Application.Dtos;
using FluentValidation;

namespace Application.Validators.Appointmnet
{
    public class AppointmentValidator : AbstractValidator<AppointmentDto>
    {
        public AppointmentValidator()
        {
            RuleFor(appointment => appointment.CustomerId)
                .NotEmpty().WithMessage("Customer Id is required.");

            RuleFor(appointment => appointment.BarberId)
                .NotEmpty().WithMessage("Barber Id is required.");

            RuleFor(appointment => appointment.AppointmentDate)
                .GreaterThan(DateTime.Now).WithMessage("Appointment date must be in the future.");

            RuleFor(appointment => appointment.Service)
                .NotEmpty().WithMessage("Service is required.")
                .MaximumLength(100).WithMessage("Service name must not exceed 100 characters.");

            RuleFor(appointment => appointment.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.00");

            RuleFor(appointment => appointment.IsCancelled)
                .NotNull().WithMessage("IsCancelled status must be provided.");
        }
    }
}
