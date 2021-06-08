using System.Collections.Generic;

namespace LoyaltyPrime.Application.Members.Commands.ImportMembers
{
    public class ImportMembersResultModel
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public List<ImportMembersDto> FailedData { get; set; }
    }
}