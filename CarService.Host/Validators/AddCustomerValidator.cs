using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarService.Models.Requests;
using FluentValidation;

namespace CarService.Host.Validators
{
    public class AddCustomerValidator : AbstractValidator<AddCustomerRequest>
    {
        public AddCustomerValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Name is required.")
                .Length(2, 100)
                .WithMessage("Name must be between 2 and 100 characters.");

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");

            RuleFor(x => x.Years)
                .InclusiveBetween(1920, 2030)
                .WithMessage("Years must be between 1920 and 2030.");
        }
    }
}