using ClienteAPI.Contracts;
using FluentValidation;

namespace ClienteAPI.Validators;

public class ClienteCreateRequestValidator : AbstractValidator<ClienteCreateRequest>
{
    public ClienteCreateRequestValidator()
    {
        RuleFor(user => user.Nombres)
            .NotEmpty()
            .Length(8, 100);
        RuleFor(user => user.Direccion)
            .NotEmpty()
            .Length(8, 100);
        RuleFor(user => user.Telefono)
            .NotEmpty()
            .Length(8, 16)
            .Matches(@"^[0-9\-\+\(\) .]+$");
        RuleFor(user => user.Password)
            .NotEmpty()
            .Length(8, 16);
        RuleFor(user => user.Identificacion)
            .Length(8, 24);
        RuleFor(user => user.TipoGeneroId)
            .GreaterThan(0);
    }
}
