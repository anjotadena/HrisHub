using Microsoft.AspNetCore.Mvc;
using HrisHub.Dal;
using HrisHub.Models;
using HrisHub.WebAPI.Jwt;

namespace HrisHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _repository;
        private readonly ITokenManager _tokenManager;

        public AuthenticationController(IAuthenticationRepository repository, ITokenManager tokenManager)
        {
            _repository = repository;
            _tokenManager = tokenManager;
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create(User user)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            user.Password = passwordHash;

            var result = _repository.RegisterUser(user);

            return result > 0 ? Ok() : BadRequest();
        }

        [HttpPost("CheckCredentials")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AuthResponse> GetDetails(User user)
        {
            var authUser = _repository.CheckCredentials(user);

            if (authUser == null)
            {
                return NotFound();
            }

            if (!BCrypt.Net.BCrypt.Verify(user.Password, authUser.Password))
            {
                return BadRequest("Invalid credentials!");
            }

            var roleName = _repository.GetUserRole(authUser.RoleId);

            var authResponse = new AuthResponse
            {
                IsAuthenticated = true,
                Role = roleName,
                Token = _tokenManager.GenerateToken(authUser, roleName)
            };

            return Ok(authResponse);
        }
    }
}
