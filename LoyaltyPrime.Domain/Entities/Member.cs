using LoyaltyPrime.Domain.Common;
using System.Collections.Generic;

namespace LoyaltyPrime.Domain.Entities
{
    public class Member : Entity
    {
        public string Name { get; set; }
        
        public string Address { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
