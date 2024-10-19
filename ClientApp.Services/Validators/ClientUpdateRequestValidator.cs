using ClientApp.Services.Contracts;
using FluentValidation;

namespace ClientApp.Services.Validators;

public class ClientUpdateRequestValidator : AbstractValidator<ClientUpdateRequest>
{
    public ClientUpdateRequestValidator()
    {
        RuleFor(user => user.FullName)
            .Length(8, 100)
            .When(user => !string.IsNullOrWhiteSpace(user.FullName));
        RuleFor(user => user.Address)
            .Length(8, 100)
            .When(user => !string.IsNullOrWhiteSpace(user.Address));
        RuleFor(user => user.Phone)
            .Length(8, 16)
            .Matches(@"^[0-9\-\+\(\) .]+$")
            .When(user => !string.IsNullOrWhiteSpace(user.Phone))
            .WithMessage("Numero de account solo deberia tener entre 0 y 16 caracteres y solo contener numeros y simbolos(- + ())");
        RuleFor(user => user.Password)
            .NotEmpty()
            .Length(8, 16)
            .When(user => !string.IsNullOrWhiteSpace(user.Password));
        RuleFor(user => user.Document)
            .Length(8, 24)
            .When(user => !string.IsNullOrWhiteSpace(user.Document));
        RuleFor(user => user.TypeGenderId)
            .InclusiveBetween(1, 2)
            .When(user => user.TypeGenderId != null);
    }
}
