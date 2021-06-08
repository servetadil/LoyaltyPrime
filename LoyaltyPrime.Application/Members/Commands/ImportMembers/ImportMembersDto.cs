using System;
using System.Collections.Generic;
using System.Text;

namespace LoyaltyPrime.Application.Members.Commands.ImportMembers
{
    public class ImportMembersDto
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
