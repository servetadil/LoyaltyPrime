using FluentValidation;
namespace LoyaltyPrime.Application.Members.Commands.CreateMember
{
    public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
    {
        public CreateMemberCommandValidator()
        {
            RuleFor(x => x.Name).MaximumLength(250).NotEmpty().NotNull();
            RuleFor(x => x.Address).MaximumLength(250).NotEmpty().NotNull();
        }
    }
}
