using AutoMapper;
using LoyaltyPrime.Domain.Entities;
using LoyaltyPrime.Domain.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LoyaltyPrime.Application.Transactions.Events
{
    public class TransactionCreatedEventHandler : INotificationHandler<TransactionCreatedEvent>
    {
        private readonly IRepository<Transaction> _repository;
        private readonly IMapper _mapper;

        public TransactionCreatedEventHandler(IRepository<Transaction> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(TransactionCreatedEvent @event, CancellationToken cancellationToken)
        {
            var transaction = _mapper.Map<TransactionCreatedEvent, Transaction>(@event);

            await _repository.CreateAsync(transaction);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
