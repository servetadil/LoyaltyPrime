using LoyaltyPrime.Domain.Common;

namespace LoyaltyPrime.Domain
{
    public class Account : Entity
    {
        public string Name { get; set; }

        public int Balance { get; set; }

        public bool IsActive { get; set; }
    }
}
