using ClientApp.Services.Contracts;
using FluentValidation;

namespace ClientApp.Services.Validators;

public class ClientCreateRequestValidator : AbstractValidator<ClientCreateRequest>
{
    public ClientCreateRequestValidator()
    {
        RuleFor(user => user.FullName)
            .NotEmpty()
            .Length(8, 100);
        RuleFor(user => user.Address)
            .NotEmpty()
            .Length(8, 100);
        RuleFor(user => user.Phone)
            .NotEmpty()
            .Length(8, 16)
            .Matches(@"^[0-9\-\+\(\) .]+$")
            .WithMessage("Numero de account solo deberia tener entre 0 y 16 caracteres y solo contener numeros y simbolos(- + ())");
        RuleFor(user => user.Password)
            .NotEmpty()
            .Length(8, 16);
        RuleFor(user => user.Document)
            .Length(8, 24);
        RuleFor(user => user.TypeGenderId)
            .InclusiveBetween(1, 2);
    }
}
