using AccountApp.Services.Contracts;
using FluentValidation;
namespace AccountApp.Services.Validators;

public class AccountCreateRequestValidator : AbstractValidator<AccountCreateRequest>
{
    public AccountCreateRequestValidator()
    {
        RuleFor(user => user.AccountNumber)
            .NotEmpty()
            .Length(8, 24)
            .Matches(@"^[a-zA-Z0-9\-]+$")
            .WithMessage("Numero de account solo deberia tener entre 0 y 24 caracteres y solo contener letras, numeros y -");
        RuleFor(user => user.OpeningBalance)
            .NotEmpty()
            .GreaterThan(0);
        RuleFor(user => user.TypeAccountId)
            .NotEmpty()
            .InclusiveBetween(1, 2);
        RuleFor(user => user.ClientId)
            .NotEmpty();
    }
}
