using Microsoft.AspNetCore.Mvc;
using WebApiUsingJWT.Attributes;
using WebApiUsingJWT.Dtos;
using WebApiUsingJWT.Repositories;

namespace WebApiUsingJWT.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequestDto model)
        {
            var response = _userRepository.Authenticate(model, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = _userRepository.RefreshToken(refreshToken, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("revoke-token")]
        public IActionResult RevokeToken(RevokeTokenRequestDto model)
        {
            // accept refresh token in request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            _userRepository.RevokeToken(token, ipAddress());
            return Ok(new { message = "Token revoked" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userRepository.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userRepository.GetById(id);
            return Ok(user);
        }

        [HttpGet("{id}/refresh-tokens")]
        public IActionResult GetRefreshTokens(int id)
        {
            var user = _userRepository.GetById(id);
            return Ok(user.RefreshTokens);
        }

        // helper methods

        private void setTokenCookie(string token)
        {
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            // get source ip address for the current request
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
