using FluentValidation;
namespace LoyaltyPrime.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(x => x.Name).MaximumLength(250).NotEmpty().NotNull();
            RuleFor(x => x.MemberID).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
