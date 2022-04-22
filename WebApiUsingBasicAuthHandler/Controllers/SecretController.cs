using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiUsingBasicAuthHandler.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    public class SecretController : ControllerBase
    {
        private readonly ILogger<SecretController> _logger;

        public SecretController(ILogger<SecretController> logger)
        {
            this._logger = logger;
        }

        // GET: api/Secret
        [HttpGet]
        public string Get()
        {
            return "secret string";
        }
    }
}
