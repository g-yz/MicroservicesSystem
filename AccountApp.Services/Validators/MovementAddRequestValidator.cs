using AccountApp.Services.Contracts;
using FluentValidation;
namespace AccountApp.Services.Validators;

public class MovementAddRequestValidator : AbstractValidator<MovementAddRequest>
{
    public MovementAddRequestValidator()
    {
        RuleFor(user => user.Value)
            .NotEmpty();
        RuleFor(user => user.AccountId)
            .NotEmpty();
    }
}
