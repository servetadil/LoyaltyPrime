using FluentValidation;

namespace LoyaltyPrime.Application.Accounts.Commands.CollectPoint
{
    public class CollectPointCommandValidator : AbstractValidator<CollectPointCommand>
    {
        public CollectPointCommandValidator()
        {
            RuleFor(x => x.AccountID).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.Point).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}

