using System.Text.Json.Serialization;

namespace WebApiUsingJWT.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }

        [JsonIgnore]
        public string? PasswordHash { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
