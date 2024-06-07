using Application.Dtos;
using FluentValidation;

namespace Application.Validators.Customer
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Id)
               .NotEmpty().WithMessage("customer Id is required.");

            RuleFor(customer => customer.FirstName)
                .NotEmpty().WithMessage("Customer first name is required.");

            RuleFor(customer => customer.LastName)
                .NotEmpty().WithMessage("Customer last name is required.");

            RuleFor(customer => customer.Email)
                .NotEmpty().WithMessage("Customer email is required.");

            RuleFor(customer => customer.Phone)
                .NotEmpty().WithMessage("Customer phone number is required.");
        }
    }
}
