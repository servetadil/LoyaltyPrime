using LoyaltyPrime.Application.Member.Commands.CreateMember;
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
            var memberId = await _mediator.Send(createMember);

            return Ok(memberId);
        }
    }
}
