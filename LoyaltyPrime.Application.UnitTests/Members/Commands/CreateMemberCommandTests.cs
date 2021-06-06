using FluentAssertions;
using LoyaltyPrime.Application.Members.Commands.CreateMember;
using LoyaltyPrime.Application.UnitTests.Base;
using LoyaltyPrime.Domain.Entities;
using LoyaltyPrime.Domain.Repository;
using LoyaltyPrime.Infrastructure.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static LoyaltyPrime.Application.Members.Commands.CreateMember.CreateMemberCommand;

namespace LoyaltyPrime.Application.UnitTests
{
    public class CreateMemberCommandTests : TestBase
    {

        protected readonly IRepository<Member> _memberRepository;
        private readonly CreateMemberCommandHandler _sut;
        private readonly CreateMemberCommandValidator _sutValidator;

        public CreateMemberCommandTests()
        {
            _memberRepository = new Repository<Member>(_context);
            _sut = new CreateMemberCommandHandler(_memberRepository);
            _sutValidator = new CreateMemberCommandValidator();
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldSuccess()
        {
            // Arrange
            var command = new CreateMemberCommand { Name = "TEST", Address = "TEST" };

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.MemberID.Should().BeGreaterThan(0);
        }


        [Theory]
        [InlineData("", "")]
        [InlineData("test", "")]
        [InlineData("", "test")]
        [InlineData(null, null)]
        public async Task Handle_GivenNotValidRequest_ShouldThrowValidationException(string name, string address)
        {
            // Arrange
            var command = new CreateMemberCommand { Name = name, Address = address };

            // Act
            var validationResult = await _sutValidator.ValidateAsync(command);

            // Assert
            validationResult.Errors.Should().NotBeEmpty();
        }
    }
}
