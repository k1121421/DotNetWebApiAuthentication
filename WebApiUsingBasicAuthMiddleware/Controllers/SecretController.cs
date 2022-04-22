using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiFour.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
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
