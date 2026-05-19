using Client.Controllers;
using FluentValidation;

namespace Client.Validators;

public sealed class GetWeatherRequestValidator : AbstractValidator<GetWeatherRequest>
{
    public GetWeatherRequestValidator()
    {
        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City name must be 100 characters or fewer.");
    }
}
