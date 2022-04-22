using System.Text.Json.Serialization;
using WebApiUsingJWT.Models;

namespace WebApiUsingJWT.Dtos
{
    public class AuthenticateResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

        public AuthenticateResponseDto(User user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            Username = user.Username;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
