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
            .WithMessage("Account number should only be between 0 and 16 characters long and only contain numbers and symbols(- + ())");
        RuleFor(user => user.Email)
            .NotEmpty()
            .Length(8, 16)
            .EmailAddress();
        RuleFor(user => user.Document)
            .Length(8, 24);
        RuleFor(user => user.TypeGenderId)
            .InclusiveBetween(1, 2);
    }
}
