using FluentAssertions;
using LoyaltyPrime.Application.Accounts.Commands.CreateAccount;
using LoyaltyPrime.Application.Common.Exceptions;
using LoyaltyPrime.Application.UnitTests.Base;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static LoyaltyPrime.Application.Accounts.Commands.CreateAccount.CreateAccountCommand;

namespace LoyaltyPrime.Application.UnitTests.Accounts.Commands
{
    public class CreateAccountCommandTests : TestBase
    {
        private readonly CreateAccountCommandHandler _sut;
        private readonly CreateAccountCommandValidator _sutValidator;
        private readonly int _testMemberID;

        public CreateAccountCommandTests()
        {
            _sut = new CreateAccountCommandHandler(_memberRepository, _accountRepository);
            _sutValidator = new CreateAccountCommandValidator();
            _testMemberID = CreateValidTestMember().Result;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldSuccess()
        {
            // Arrange
            var command = new CreateAccountCommand { Name = "TEST", MemberID = _testMemberID };

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.AccountID.Should().BeGreaterThan(0);
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
        [InlineData("")]
        [InlineData(null)]
        public async Task Handle_GivenNotValidRequest_ShouldThrowValidationException(string name)
        {
            // Arrange
            var command = new CreateAccountCommand { Name = name, MemberID = _testMemberID };

            // Act
            var validationResult = await _sutValidator.ValidateAsync(command);

            // Assert
            validationResult.Errors.Should().NotBeEmpty();
        }
    }
}
