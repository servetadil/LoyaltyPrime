using FluentAssertions;
using LoyaltyPrime.Application.Accounts.Commands.CreateAccount;
using LoyaltyPrime.Application.Common.Exceptions;
using LoyaltyPrime.Application.UnitTests.Base;
using LoyaltyPrime.Domain.Entities;
using LoyaltyPrime.Domain.Repository;
using LoyaltyPrime.Infrastructure.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static LoyaltyPrime.Application.Accounts.Commands.CreateAccount.CreateAccountCommand;

namespace LoyaltyPrime.Application.UnitTests
{
    public class CreateAccountCommandTests : TestBase
    {

        protected readonly IRepository<Account> _accountRepository;
        private readonly CreateAccountCommandHandler _sut;
        private readonly CreateAccountCommandValidator _sutValidator;

        public CreateAccountCommandTests()
        {
            _accountRepository = new Repository<Account>(_context);
            _sut = new CreateAccountCommandHandler(_accountRepository);
            _sutValidator = new CreateAccountCommandValidator();
        }

        [Fact]
        public void Handle_GivenMemberId_ShouldThrownNotFoundException()
        {
            // Arrange
            var command = new CreateAccountCommand() { Name = "Test", MemberID = 999999 };

            // Act
            Func<Task> func = async () => { await _sut.Handle(command, CancellationToken.None); };

            // Assert
            func.Should().Throw<NotFoundException>();
        }


        [Theory]
        [InlineData("", 10)]
        [InlineData(null, 10)]
        public async Task Handle_GivenNotValidRequest_ShouldThrowValidationException(string name, int memberId)
        {
            // Arrange
            var command = new CreateAccountCommand { Name = name, MemberID = memberId };

            // Act
            var validationResult = await _sutValidator.ValidateAsync(command);

            // Assert
            validationResult.Errors.Should().NotBeEmpty();
        }
    }
}
