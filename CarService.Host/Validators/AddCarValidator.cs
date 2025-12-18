
using CarService.Models.Requests;
using FluentValidation;

namespace CarService.Host.Validators
{
    public class AddCarValidator : AbstractValidator<AddCarRequest>
    {
        public AddCarValidator()
        {
            RuleFor(x => x.Model)
                .NotNull()
                .NotEmpty()
                .WithMessage("Model is required.")
                .Length(1, 100)
                .WithMessage("Model must be between 1 and 100 characters.");

            RuleFor(x => x.Year)
                .InclusiveBetween(1886, DateTime.Now.Year + 1)
                .WithMessage($"Year must be between 1886 and {DateTime.Now.Year + 1}.");


        }
    }

}