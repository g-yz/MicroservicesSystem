using AccountApp.Data.Models;
using FluentValidation;
namespace AccountApp.Services.Validators;

public class MovementReporteFilterValidator : AbstractValidator<MovementReporteFilter>
{
    public MovementReporteFilterValidator()
    {
        RuleFor(x => x).Must(x => x.StartDate == default(DateTime) || x.EndDate == default(DateTime) || x.EndDate > x.StartDate)
            .When(user => user.StartDate != null && user.EndDate != null)
            .WithMessage("La date de inicio debe ser menor a la date fin");
        RuleFor(user => user.AccountNumber)
            .Length(8, 24)
            .Matches(@"^[a-zA-Z0-9\-]+$")
            .When(user => user.AccountNumber != null)
            .WithMessage("Numero de account solo deberia tener entre 0 y 24 caracteres y solo contener letras, numeros y -");
    }
}
