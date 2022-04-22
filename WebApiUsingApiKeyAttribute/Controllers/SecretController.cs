using Microsoft.AspNetCore.Mvc;
using WebApiUsingApiKeyAttribute.Attributes;

namespace WebApiUsingApiKeyAttribute.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiKey]
    [ApiVersion("1.0")]
    public class SecretController : ControllerBase
    {
        private readonly ILogger<SecretController> _logger;

        public SecretController(ILogger<SecretController> logger)
        {
            this._logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "secret string";
        }
    }
}
