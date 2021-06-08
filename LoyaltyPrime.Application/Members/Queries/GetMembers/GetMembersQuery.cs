using AutoMapper;
using LoyaltyPrime.Application.Members.Commands.ImportMembers;
using LoyaltyPrime.Domain.Entities;
using LoyaltyPrime.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LoyaltyPrime.Application.Members.Queries.GetMembers
{
    public class GetMembersQuery : GetMemberQueryFilters, IRequest<GetMembersViewModel>
    {
        public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, GetMembersViewModel>
        {
            private readonly IRepository<Member> _memberRepository;
            IMapper _mapper;

            public GetMembersQueryHandler(
               IRepository<Member> memberRepository,
               IMapper mapper)
            {
                _memberRepository = memberRepository;
                _mapper = mapper;
            }

            public async Task<GetMembersViewModel> Handle(GetMembersQuery filters, CancellationToken cancellationToken)
            {
                var query = _memberRepository.GetQueryable();

                if (!string.IsNullOrEmpty(filters.Name))
                {
                    query = query.Where(x => x.Name.ToLower() == filters.Name.ToLower());
                }

                if (!string.IsNullOrEmpty(filters.Address))
                {
                    query = query.Where(x => x.Address.ToLower().Contains(filters.Address.ToLower()));
                }

                if (filters.MinAccountPoint > 0)
                {
                    query = query.Select(newObject => new Member
                    {
                        Name = newObject.Name,
                        Address = newObject.Address,
                        Accounts = newObject.Accounts.Where(x => x.Balance > filters.MinAccountPoint && x.IsActive == true).ToList()
                    });
                    query = query.Where(x => x.Accounts.Count > 0);
                }

                return new GetMembersViewModel()
                {
                    Members = _mapper.Map<IEnumerable<GetMembersViewModel.ExportMembersDto>>(query.ToList())
                };
            }
        }
    }
}
