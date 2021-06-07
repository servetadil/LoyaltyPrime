using LoyaltyPrime.Domain.Repository;
using LoyaltyPrime.Domain.Entities;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using LoyaltyPrime.Application.Common.Exceptions;

namespace LoyaltyPrime.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<CreateAccountResultModel>
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public int MemberID { get; set; }

        public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountResultModel>
        {
            private readonly IRepository<Member> _memberRepository;

            private readonly IRepository<Account> _accountRepository;

            public CreateAccountCommandHandler(
                IRepository<Member> memberRepository,
                IRepository<Account> accountRepository)
            {
                _memberRepository = memberRepository;
                _accountRepository = accountRepository;
            }

            public async Task<CreateAccountResultModel> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
            {
                var member = await _memberRepository.GetAsync(request.MemberID);

                if (member == null)
                    throw new NotFoundException(nameof(Member), request.MemberID.ToString());


                var account = new Account()
                {
                    Name = request.Name,
                    IsActive = request.IsActive,
                    MemberID = member.Id
                };

            await _accountRepository.CreateAsync(account);

            await _accountRepository.SaveChangesAsync(cancellationToken);

                return new CreateAccountResultModel() { AccountID = account.Id };
        }
    }
}
}
