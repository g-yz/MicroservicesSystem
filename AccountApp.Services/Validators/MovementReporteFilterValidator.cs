using AccountApp.Data.Models;
using FluentValidation;
namespace AccountApp.Services.Validators;

public class MovementReporteFilterValidator : AbstractValidator<MovementReporteFilter>
{
    public MovementReporteFilterValidator()
    {
        RuleFor(x => x).Must(x => x.StartDate == default(DateTime) || x.EndDate == default(DateTime) || x.EndDate > x.StartDate)
            .When(user => user.StartDate != null && user.EndDate != null)
            .WithMessage("The start date must be less than the end date");
        RuleFor(user => user.AccountNumber)
            .Length(8, 24)
            .Matches(@"^[a-zA-Z0-9\-]+$")
            .When(user => user.AccountNumber != null)
            .WithMessage("Account number should only be between 0 and 24 characters and only contain letters, numbers and -");
    }
}
