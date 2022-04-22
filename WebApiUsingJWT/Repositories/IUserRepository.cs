using WebApiUsingJWT.Dtos;
using WebApiUsingJWT.Models;

namespace WebApiUsingJWT.Repositories
{
    public interface IUserRepository
    {
        AuthenticateResponseDto Authenticate(AuthenticateRequestDto model, string ipAddress);
        AuthenticateResponseDto RefreshToken(string token, string ipAddress);
        void RevokeToken(string token, string ipAddress);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
