using FluentAssertions;
using LoyaltyPrime.Application.Accounts.Commands.CollectPoint;
using LoyaltyPrime.Application.Accounts.Commands.RedeemPoint;
using LoyaltyPrime.Application.Common.Exceptions;
using LoyaltyPrime.Application.UnitTests.Base;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static LoyaltyPrime.Application.Accounts.Commands.CollectPoint.RedeemPointCommand;

namespace LoyaltyPrime.Application.UnitTests.Accounts.Commands
{
    public class RedeemPointCommandTests : TestBase
    {
        private readonly RedeemPointCommandHandler _sut;
        private readonly RedeemPointCommandValidator _sutValidator;
        private readonly int _testAccountID;
        private readonly int _testInactiveAccountID;

        public RedeemPointCommandTests()
        {
            _sut = new RedeemPointCommandHandler(_accountRepository, _mockMediator.Object);
            _sutValidator = new RedeemPointCommandValidator();
            _testAccountID = CreateValidTestAccount().Result;
            _testInactiveAccountID = CreateInActiveTestAccount().Result;
        }

        [Fact]
        public void Handle_GivenInactiveAccount_ShouldThrownBusinessRuleException()
        {
            // Arrange
            var command = new RedeemPointCommand { AccountID = _testInactiveAccountID, Description = "Test", Point = 10 };

            // Act
            Func<Task> func = async () => { await _sut.Handle(command, CancellationToken.None); };

            // Assert
            func.Should().Throw<BusinessRuleException>();
        }

        [Fact]
        public void Handle_GivenEmptyBalance_ShouldThrownBusinessRuleException()
        {
            // Arrange
            var command = new RedeemPointCommand { AccountID = _testAccountID, Description = "Test", Point = 10 };

            // Act
            Func<Task> func = async () => { await _sut.Handle(command, CancellationToken.None); };

            // Assert
            func.Should().Throw<BusinessRuleException>().WithMessage("Insufficient balance on this account.");
        }

        [Fact]
        public void Handle_GivenInvalidAccount_ShouldThrownNotFoundException()
        {
            // Arrange
            var command = new RedeemPointCommand { AccountID = 99999, Description = "Test", Point = 10 };

            // Act
            Func<Task> func = async () => { await _sut.Handle(command, CancellationToken.None); };

            // Assert
            func.Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task Handle_GivenInvalidRequest_ShouldThrownValidationException()
        {
            // Arrange
            var command = new RedeemPointCommand { AccountID = _testAccountID, Description = "Test", Point = 0 };

            // Act
            var validationResult = await _sutValidator.ValidateAsync(command);

            // Assert
            validationResult.Errors.Should().NotBeEmpty();
        }
    }
}
