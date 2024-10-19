using ClienteApp.Services.Contracts;
using FluentValidation;

namespace ClienteApp.Services.Validators;

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
            .Matches(@"^[0-9\-\+\(\) .]+$")
            .WithMessage("Numero de cuenta solo deberia tener entre 0 y 16 caracteres y solo contener numeros y simbolos(- + ())");
        RuleFor(user => user.Password)
            .NotEmpty()
            .Length(8, 16);
        RuleFor(user => user.Identificacion)
            .Length(8, 24);
        RuleFor(user => user.TipoGeneroId)
            .InclusiveBetween(1, 2);
    }
}
