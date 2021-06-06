
using MediatR;

namespace LoyaltyPrime.Application.Transactions.Events
{
    public class TransactionCreatedEvent : INotification
    {
        public int AccountID { get; set; }

        public int Point { get; set; }

        public string Description { get; set; }
    }
}
