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
            .WithMessage("Account number should only be between 0 and 24 characters and only contain letters, numbers and -");
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
