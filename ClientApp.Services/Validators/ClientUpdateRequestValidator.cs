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
            .WithMessage("Account number should only be between 0 and 16 characters long and only contain numbers and symbols(- + ())");
        RuleFor(user => user.Email)
            .NotEmpty()
            .Length(8, 16)
            .EmailAddress()
            .When(user => !string.IsNullOrWhiteSpace(user.Email));
        RuleFor(user => user.Document)
            .Length(8, 24)
            .When(user => !string.IsNullOrWhiteSpace(user.Document));
        RuleFor(user => user.TypeGenderId)
            .InclusiveBetween(1, 2)
            .When(user => user.TypeGenderId != null);
    }
}
