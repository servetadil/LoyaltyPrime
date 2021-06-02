
using LoyaltyPrime.Domain.Common;

namespace LoyaltyPrime.Domain.Entities
{
    public class Transacation : Entity
    {
        public int AccountID { get; set;}

        public int Balance { get; set; }
    }
}
