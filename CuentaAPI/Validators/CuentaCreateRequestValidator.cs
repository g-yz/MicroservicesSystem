using CuentaAPI.Contracts;
using FluentValidation;
namespace CuentaAPI.Validators;

public class CuentaCreateRequestValidator : AbstractValidator<CuentaCreateRequest>
{
    public CuentaCreateRequestValidator()
    {
        RuleFor(user => user.NumeroCuenta)
            .NotEmpty()
            .Length(8, 24)
            .Matches(@"^[a-zA-Z0-9\-]+$")
            .WithMessage("Numero de cuenta solo deberia tener entre 0 y 24 caracteres y solo contener letras, numeros y -");
        RuleFor(user => user.SaldoInicial)
            .NotEmpty()
            .GreaterThan(0);
        RuleFor(user => user.TipoCuentaId)
            .NotEmpty()
            .InclusiveBetween(1, 2);
        RuleFor(user => user.ClienteId)
            .NotEmpty();
    }
}
