using Microsoft.AspNetCore.Mvc;
using NLog;

namespace LoyaltyPrime.Web.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        protected ApiController()
        {

        }
    }
}
