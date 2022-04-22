using Microsoft.AspNetCore.Mvc;

namespace WebApiUsingApiKeyMiddleware.Controllers
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
