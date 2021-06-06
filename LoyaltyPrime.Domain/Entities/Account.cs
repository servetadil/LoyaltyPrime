using LoyaltyPrime.Domain.Common;

namespace LoyaltyPrime.Domain.Entities
{
    public class Account : Entity
    {
        public string Name { get; set; }

        public int Balance { get; set; }

        public bool IsActive { get; set; }

        public int MemberID { get; set; }
    }
}
