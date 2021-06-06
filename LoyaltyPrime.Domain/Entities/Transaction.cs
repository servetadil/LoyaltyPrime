
using LoyaltyPrime.Domain.Common;

namespace LoyaltyPrime.Domain.Entities
{
    public class Transaction : Entity
    {
        public int AccountID { get; set;}

        public string Description { get; set; }

        public int Amount { get; set; }
    }
}
