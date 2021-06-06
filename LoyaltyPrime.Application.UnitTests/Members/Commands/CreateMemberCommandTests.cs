using LoyaltyPrime.Application.Member.Commands.CreateMember;
using LoyaltyPrime.Application.UnitTests.Base;
using LoyaltyPrime.Domain.Entities;
using LoyaltyPrime.Domain.Repository;
using LoyaltyPrime.Infrastructure.Repositories;
using MediatR;
using Moq;
using System;
using System.Threading;
using Xunit;
using static LoyaltyPrime.Application.Member.Commands.CreateMember.CreateMemberCommand;

namespace LoyaltyPrime.Application.UnitTests
{
    public class CreateMemberCommandTests : TestBase
    {

        protected readonly IRepository<Members> _memberRepository;
        private readonly CreateMemberCommandHandler _sut;
        private readonly CreateMemberCommandValidator _sutValidator;

        public CreateMemberCommandTests()
        {
            _memberRepository = new Repository<Members>(_context);
            _sut = new CreateMemberCommandHandler(_memberRepository);
            _sutValidator = new CreateMemberCommandValidator();
        }
    }
}
