using CuentaAPI.Contracts;
using FluentValidation;
namespace CuentaAPI.Validators;

public class MovimientoReporteFilterValidator : AbstractValidator<MovimientoReporteFilter>
{
    public MovimientoReporteFilterValidator()
    {
        RuleFor(x => x).Must(x => x.FechaInicio == default(DateTime) || x.FechaFin == default(DateTime) || x.FechaFin > x.FechaInicio)
            .When(user => user.FechaInicio != null && user.FechaFin != null)
            .WithMessage("La fecha de inicio debe ser menor a la fecha fin");
        RuleFor(user => user.NumeroCuenta)
            .Length(8, 24)
            .Matches(@"^[a-zA-Z0-9\-]+$")
            .When(user => user.NumeroCuenta != null)
            .WithMessage("Numero de cuenta solo deberia tener entre 0 y 24 caracteres y solo contener letras, numeros y -");
    }
}
