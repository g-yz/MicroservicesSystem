using CuentaAPI.Contracts;
using FluentValidation;
namespace CuentaAPI.Validators;

public class MovimientoAddRequestValidator : AbstractValidator<MovimientoAddRequest>
{
    public MovimientoAddRequestValidator()
    {
        RuleFor(user => user.Valor)
            .NotEmpty();
        RuleFor(user => user.CuentaId)
            .NotEmpty();
    }
}
