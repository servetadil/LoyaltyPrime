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
    }
}
