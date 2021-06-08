using System;
using System.Collections.Generic;
using System.Text;

namespace LoyaltyPrime.Application.Members.Queries.GetMembers
{
    public class GetMemberQueryFilters
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public int MinAccountPoint { get; set; }

    }
}
