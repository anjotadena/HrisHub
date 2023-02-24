using HrisHub.Models;

namespace HrisHub.Dal
{
    public interface IAuthenticationRepository
    {
        int RegisterUser(User user);

        User? CheckCredentials(User user);
    }
}
