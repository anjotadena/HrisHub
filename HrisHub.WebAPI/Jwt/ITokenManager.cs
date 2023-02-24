using HrisHub.Models;

namespace HrisHub.WebAPI.Jwt
{
    public interface ITokenManager
    {
        string GenerateToken(User user);
    }
}
