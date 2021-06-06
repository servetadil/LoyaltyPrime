using FluentValidation;
using LoyaltyPrime.Application.Accounts.Commands.CollectPoint;

namespace LoyaltyPrime.Application.Accounts.Commands.RedeemPoint
{
    public class RedeemPointCommandValidator : AbstractValidator<RedeemPointCommand>
    {
        public RedeemPointCommandValidator()
        {
            RuleFor(x => x.AccountID).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.Point).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
