using AutoMapper;
using LoyaltyPrime.Domain.Entities;
using LoyaltyPrime.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace LoyaltyPrime.Application.Members.Commands.ImportMembers
{
    public class ImportMembersCommand : IRequest<ImportMembersResultModel>
    {
        public List<ImportMembersDto> ImportData { get; set; }

        public class ImportMembersCommandHandler : IRequestHandler<ImportMembersCommand, ImportMembersResultModel>
        {
            IRepository<Account> _accountRepository;
            IRepository<Member> _memberRepository;
            IMapper _mapper;

            public ImportMembersCommandHandler(
                IRepository<Account> accountRepository,
                IRepository<Member> memberRepository,
                IMapper mapper)
            {
                _memberRepository = memberRepository;
                _accountRepository = accountRepository;
                _mapper = mapper;
            }

            public async Task<ImportMembersResultModel> Handle(ImportMembersCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    using (var transaction = await _accountRepository.BeginTransactionAsync())
                    {
                        var memberslist = _mapper.Map<List<Member>>(request.ImportData);

                        await _memberRepository.BulkInsertAsync(memberslist);

                        await _accountRepository.CommitTransactionAsync();
                    }
                }
                catch (Exception ex)
                {
                    await _accountRepository.RollbackTransactionAsync();
                    throw ex;
                }

                return new ImportMembersResultModel() { IsSuccess = true, Message = "Successfully imported" };
            }
        }
    }
}
