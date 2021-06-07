using LoyaltyPrime.Domain.Repository;
using LoyaltyPrime.Domain.Entities;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace LoyaltyPrime.Application.Members.Commands.CreateMember
{
    public class CreateMemberCommand : IRequest<CreateMemberResultModel>
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, CreateMemberResultModel>
        {
            private readonly IRepository<Member> _repository;

            public CreateMemberCommandHandler(IRepository<Member> repository)
            {
                _repository = repository;
            }

            public async Task<CreateMemberResultModel> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
            {
                var member = new Member()
                {
                    Name = request.Name,
                    Address = request.Address
                };

                await _repository.CreateAsync(member);

                await _repository.SaveChangesAsync(cancellationToken);

                return new CreateMemberResultModel() { MemberID = member.Id };
            }
        }
    }
}
