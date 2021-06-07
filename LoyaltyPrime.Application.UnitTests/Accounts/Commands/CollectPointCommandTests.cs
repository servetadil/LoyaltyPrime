using FluentAssertions;
using LoyaltyPrime.Application.Accounts.Commands.CollectPoint;
using LoyaltyPrime.Application.Common.Exceptions;
using LoyaltyPrime.Application.UnitTests.Base;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static LoyaltyPrime.Application.Accounts.Commands.CollectPoint.CollectPointCommand;

namespace LoyaltyPrime.Application.UnitTests.Accounts.Commands
{
    public class CollectPointCommandTests : TestBase
    {
        private readonly CollectPointCommandHandler _sut;
        private readonly CollectPointCommandValidator _sutValidator;
        private readonly int _testAccountID;

        public CollectPointCommandTests()
        {
            _sut = new CollectPointCommandHandler(_accountRepository, _mockMediator.Object);
            _sutValidator = new CollectPointCommandValidator();
            _testAccountID = CreateValidTestAccount().Result;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldSuccess()
        {
            // Arrange
            var command = new CollectPointCommand { AccountID = _testAccountID, Description = "Test", Point = 10 };

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Balance.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Handle_GivenInvalidAccount_ShouldThrownNotFoundException()
        {
            // Arrange
            var command = new CollectPointCommand { AccountID = 99999, Description = "Test", Point = 0 };

            // Act
            Func<Task> func = async () => { await _sut.Handle(command, CancellationToken.None); };

            // Assert
            func.Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task Handle_GivenInvalidRequest_ShouldThrownValidationException()
        {
            // Arrange
            var command = new CollectPointCommand { AccountID = _testAccountID, Description = "Test", Point = 0 };

            // Act
            var validationResult = await _sutValidator.ValidateAsync(command);

            // Assert
            validationResult.Errors.Should().NotBeEmpty();
        }
    }
}
