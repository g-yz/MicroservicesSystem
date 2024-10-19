using CuentaApp.Services.Contracts;
using FluentValidation;
namespace CuentaApp.Services.Validators;

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
