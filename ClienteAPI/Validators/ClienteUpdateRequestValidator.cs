using ClienteAPI.Contracts;
using FluentValidation;

namespace ClienteAPI.Validators;

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
            .When(user => !string.IsNullOrWhiteSpace(user.Telefono));
        RuleFor(user => user.Password)
            .NotEmpty()
            .Length(8, 16)
            .When(user => !string.IsNullOrWhiteSpace(user.Password));
        RuleFor(user => user.Identificacion)
            .Length(8, 24)
            .When(user => !string.IsNullOrWhiteSpace(user.Identificacion));
        RuleFor(user => user.TipoGeneroId)
            .GreaterThan(0)
            .When(user => user.TipoGeneroId != null);
    }
}
