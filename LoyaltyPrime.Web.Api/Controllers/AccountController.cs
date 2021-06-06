using LoyaltyPrime.Application.Accounts.Commands.CollectPoint;
using LoyaltyPrime.Application.Accounts.Commands.CreateAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LoyaltyPrime.Web.Api.Controllers
{
    [ApiController]
    public class AccountController : ApiController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create new account for defined member
        /// </summary>
        ///
        [HttpPost]
        [Route("create-account")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand createAccount)
        {
            var result = await _mediator.Send(createAccount);

            return Ok(result);
        }

        /// <summary>
        /// Member collects points to an existing account
        /// </summary>
        ///
        [HttpPost]
        [Route("collect-point")]
        [Produces("application/json")]
        public async Task<IActionResult> CollectPoint([FromBody] CollectPointCommand collectPoint)
        {
            var result = await _mediator.Send(collectPoint);

            return Ok(result);
        }

        /// <summary>
        /// Member redeem points from existing account
        /// </summary>
        ///
        [HttpPost]
        [Route("redeem-point")]
        [Produces("application/json")]
        public async Task<IActionResult> RedeemPoint([FromBody] RedeemPointCommand redeemPoint)
        {
            var result = await _mediator.Send(redeemPoint);

            return Ok(result);
        }
    }
}
