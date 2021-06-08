using AutoMapper;
using LoyaltyPrime.Application.Accounts.Commands.CreateAccount;
using LoyaltyPrime.Application.Members.Commands.CreateMember;
using LoyaltyPrime.Domain.Entities;
using LoyaltyPrime.Domain.Repository;
using LoyaltyPrime.Infrastructure;
using LoyaltyPrime.Infrastructure.Repositories;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using static LoyaltyPrime.Application.Accounts.Commands.CreateAccount.CreateAccountCommand;
using static LoyaltyPrime.Application.Members.Commands.CreateMember.CreateMemberCommand;

namespace LoyaltyPrime.Application.UnitTests.Base
{
    public class TestBase : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IRepository<Member> _memberRepository;
        protected readonly IRepository<Account> _accountRepository;
        protected readonly Mock<IMediator> _mockMediator;

        public TestBase()
        {
            _context = ApplicationDbContextFactory.Create();
            _memberRepository = new Repository<Member>(_context);
            _accountRepository = new Repository<Account>(_context);
            _mockMediator = new Mock<IMediator>();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(_context);
        }

        public async Task<int> CreateValidTestMember()
        {
            var memberCommand = new CreateMemberCommand() { Name = "Test", Address = "test" };
            var memberCommandHandler = new CreateMemberCommandHandler(_memberRepository);

            var result = await memberCommandHandler.Handle(memberCommand, CancellationToken.None);

            return result.MemberID;
        }

        public async Task<int> CreateValidTestAccount()
        {
            return await CreateTestAccount(true);
        }

        public async Task<int> CreateInActiveTestAccount()
        {
            return await CreateTestAccount(false);
        }

        private async Task<int> CreateTestAccount(bool isActive)
        {
            var member = await CreateValidTestMember();

            var accountCommand = new CreateAccountCommand { Name = "TEST", MemberID = member, IsActive = isActive };
            var accountCommandHandler = new CreateAccountCommandHandler(_memberRepository, _accountRepository);

            var result = await accountCommandHandler.Handle(accountCommand, CancellationToken.None);

            return result.AccountID;
        }
    }
}
