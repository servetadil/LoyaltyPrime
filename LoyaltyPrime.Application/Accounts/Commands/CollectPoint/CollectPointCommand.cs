using LoyaltyPrime.Domain.Repository;
using LoyaltyPrime.Domain.Entities;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using LoyaltyPrime.Application.Common.Exceptions;
using LoyaltyPrime.Application.Transactions.Events;

namespace LoyaltyPrime.Application.Accounts.Commands.CollectPoint
{
    public class CollectPointCommand : IRequest<CollectPointResultModel>
    {
        public int AccountID { get; set; }

        public int Point { get; set; }

        public string Description { get; set; }

        public class CollectPointCommandHandler : IRequestHandler<CollectPointCommand, CollectPointResultModel>
        {
            private readonly IRepository<Account> _repository;

            private readonly IMediator _mediator;

            public CollectPointCommandHandler(IRepository<Account> repository, IMediator mediator)
            {
                _repository = repository;

                _mediator = mediator;
            }

            public async Task<CollectPointResultModel> Handle(CollectPointCommand request, CancellationToken cancellationToken)
            {
                var account = await _repository.GetAsync(request.AccountID);

                if (account == null)
                    throw new NotFoundException(nameof(Account), request.AccountID.ToString());

                account.Balance = account.Balance + request.Point;

                await _repository.UpdateAsync(account);
                await _repository.SaveChangesAsync(cancellationToken);


                var @event = new TransactionCreatedEvent
                {
                    AccountID = account.Id,
                    Description = request.Description,
                    Point = request.Point
                };

                await _mediator.Publish(@event, cancellationToken);

                return new CollectPointResultModel() { AccountID = account.Id, Balance = account.Balance };
            }
        }
    }
}
