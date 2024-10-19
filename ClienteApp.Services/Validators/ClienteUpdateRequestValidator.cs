using ClienteApp.Services.Contracts;
using FluentValidation;

namespace ClienteApp.Services.Validators;

public class ClienteUpdateRequestValidator : AbstractValidator<ClienteUpdateRequest>
{
    public ClienteUpdateRequestValidator()
    {
        RuleFor(user => user.Nombres)
            .Length(8, 100)
            .When(user => !string.IsNullOrWhiteSpace(user.Nombres));
        RuleFor(user => user.Direccion)
            .Length(8, 100)
            .When(user => !string.IsNullOrWhiteSpace(user.Direccion));
        RuleFor(user => user.Telefono)
            .Length(8, 16)
            .Matches(@"^[0-9\-\+\(\) .]+$")
            .When(user => !string.IsNullOrWhiteSpace(user.Telefono))
            .WithMessage("Numero de cuenta solo deberia tener entre 0 y 16 caracteres y solo contener numeros y simbolos(- + ())");
        RuleFor(user => user.Password)
            .NotEmpty()
            .Length(8, 16)
            .When(user => !string.IsNullOrWhiteSpace(user.Password));
        RuleFor(user => user.Identificacion)
            .Length(8, 24)
            .When(user => !string.IsNullOrWhiteSpace(user.Identificacion));
        RuleFor(user => user.TipoGeneroId)
            .InclusiveBetween(1, 2)
            .When(user => user.TipoGeneroId != null);
    }
}
