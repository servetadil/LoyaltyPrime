using LoyaltyPrime.Domain.Repository;
using LoyaltyPrime.Domain.Entities;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace LoyaltyPrime.Application.Member.Commands.CreateMember
{
    public class CreateMemberCommand : IRequest
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand>
        {
            private readonly IRepository<Members> _repository;

            public CreateMemberCommandHandler(IRepository<Members> repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
            {
                var member = new Members()
                {
                    Name = request.Name,
                    Address = request.Address
                };

                await _repository.CreateAsync(member);

                await _repository.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
