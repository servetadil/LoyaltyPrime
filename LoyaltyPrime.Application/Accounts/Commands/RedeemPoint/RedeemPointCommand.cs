using LoyaltyPrime.Domain.Repository;
using LoyaltyPrime.Domain.Entities;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using LoyaltyPrime.Application.Common.Exceptions;
using LoyaltyPrime.Application.Transactions.Events;
using LoyaltyPrime.Application.Accounts.Commands.RedeemPoint;

namespace LoyaltyPrime.Application.Accounts.Commands.CollectPoint
{
    public class RedeemPointCommand : IRequest<RedeemPointResultModel>
    {
        public int AccountID { get; set; }

        public int Point { get; set; }

        public string Description { get; set; }

        public class RedeemPointCommandHandler : IRequestHandler<RedeemPointCommand, RedeemPointResultModel>
        {
            private readonly IRepository<Account> _repository;

            private readonly IMediator _mediator;

            public RedeemPointCommandHandler(IRepository<Account> repository, IMediator mediator)
            {
                _repository = repository;
                _mediator = mediator;
            }

            public async Task<RedeemPointResultModel> Handle(RedeemPointCommand request, CancellationToken cancellationToken)
            {
                var account = await _repository.GetAsync(request.AccountID);

                if (account == null)
                    throw new NotFoundException(nameof(Account), request.AccountID.ToString());

                if (!account.IsActive)
                    throw new BusinessRuleException("Points cannot be redeemed from an inactive account.");

                if (account.Balance == 0 || account.Balance < request.Point)
                    throw new BusinessRuleException("Insufficient balance on this account.");


                account.Balance = account.Balance - request.Point;

                await _repository.UpdateAsync(account);
                await _repository.SaveChangesAsync(cancellationToken);


                var @event = new TransactionCreatedEvent
                {
                    AccountID = account.Id,
                    Description = request.Description,
                    Point = -request.Point
                };

                await _mediator.Publish(@event, cancellationToken);

                return new RedeemPointResultModel() { AccountID = account.Id, Balance = account.Balance };
            }
        }
    }
}
