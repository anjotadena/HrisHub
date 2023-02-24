using HrisHub.Models;

namespace HrisHub.Dal
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly HrisHubDbContext _dbContext;

        public AuthenticationRepository(HrisHubDbContext context)
        {
            _dbContext = context;
        }

        User? IAuthenticationRepository.CheckCredentials(User user)
        {
            var credentials = _dbContext.Users.SingleOrDefault(u => u.Email == user.Email);

            return credentials;
        }

        int IAuthenticationRepository.RegisterUser(User user)
        {
            _dbContext.Users.Add(user);

            return _dbContext.SaveChanges();
        }
    }
}
