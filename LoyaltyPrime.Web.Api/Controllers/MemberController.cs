using LoyaltyPrime.Application.Members.Commands.CreateMember;
using LoyaltyPrime.Application.Members.Commands.ImportMembers;
using LoyaltyPrime.Application.Members.Queries.GetMembers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LoyaltyPrime.Web.Api.Controllers
{
    [ApiController]
    public class MemberController : ApiController
    {
        private readonly IMediator _mediator;

        public MemberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create new member
        /// </summary>
        ///
        [HttpPost]
        [Route("create-member")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateMember([FromBody] CreateMemberCommand createMember)
        {
            var result = await _mediator.Send(createMember);

            return Ok(result);
        }

        /// <summary>
        /// Import new members
        /// </summary>
        ///
        [HttpPost]
        [Route("import-members")]
        [Produces("application/json")]
        public async Task<IActionResult> ImportMembers([FromBody] ImportMembersCommand importMembers)
        {
            var result = await _mediator.Send(importMembers);

            return Ok(result);
        }

        /// <summary>
        /// Export members with given criterias
        /// </summary>
        ///
        [HttpGet]
        [Route("export-members")]
        [Produces("application/json")]
        public async Task<IActionResult> ExportMembers([FromQuery] GetMembersQuery memberQuery)
        {
            var result = await _mediator.Send(memberQuery);

            return Ok(result);
        }
    }
}
