using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiUsingBasicAuthAttribute.Attributes;

namespace WebApiFour.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [BasicAuthorize]
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

        // GET: api/Secret
        [AllowAnonymous]
        [HttpPost]
        public string Post()
        {
            return "secret string";
        }
    }
}
