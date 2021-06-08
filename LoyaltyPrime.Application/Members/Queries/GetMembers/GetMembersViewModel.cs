using System.Collections.Generic;

namespace LoyaltyPrime.Application.Members.Queries.GetMembers
{
    public class GetMembersViewModel
    {
        public IEnumerable<ExportMembersDto> Members { get; set; }

        public class ExportMembersDto
        {
            public string Name { get; set; }
            public string Address { get; set; }

            public List<AccountsDto> Accounts { get; set; }

            public class AccountsDto
            {
                public string Name { get; set; }

                public int Balance { get; set; }

                public string Status { get; set; }
            }
        }
    }
}