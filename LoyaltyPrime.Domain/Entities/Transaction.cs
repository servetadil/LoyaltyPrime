
using LoyaltyPrime.Domain.Common;

namespace LoyaltyPrime.Domain.Entities
{
    public class Transaction : Entity
    {
        public int AccountID { get; set;}

        public int Balance { get; set; }
    }
}
