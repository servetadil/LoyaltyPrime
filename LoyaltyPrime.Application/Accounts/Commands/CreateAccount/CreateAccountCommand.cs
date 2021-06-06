using LoyaltyPrime.Domain.Repository;
using LoyaltyPrime.Domain.Entities;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using LoyaltyPrime.Application.Common.Exceptions;

namespace LoyaltyPrime.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<CreateAccountViewModel>
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public int MemberID { get; set; }

        public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountViewModel>
        {
            private readonly IRepository<Account> _repository;

            public CreateAccountCommandHandler(IRepository<Account> repository)
            {
                _repository = repository;
            }

            public async Task<CreateAccountViewModel> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
            {
                var member = await _repository.GetAsync(request.MemberID);

                if (member == null)
                    throw new NotFoundException(nameof(Member), request.MemberID.ToString());


                var account = new Account()
                {
                    Name = request.Name,
                    IsActive = false,
                    MemberID = member.Id
                };

                await _repository.CreateAsync(account);

                await _repository.SaveChangesAsync(cancellationToken);

                return new CreateAccountViewModel() { AccountID = account.Id };
            }
        }
    }
}
