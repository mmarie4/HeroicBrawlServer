using System.Threading.Tasks;
using HeroicBrawlServer.Controllers.Models.Users;
using HeroicBrawlServer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace HeroicBrawlServer.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        ///     Logs in an user
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<UserResponse> Login([FromBody] LoginRequest loginRequest)
        {
            var loginParameter = LoginRequest.ToParameter(loginRequest);
            var (user, token) = await _userService.Login(loginParameter);

            var result = UserResponse.FromEntity(user, token);
            result.Token = token;
            return result;
        }

        /// <summary>
        ///     Register a new user
        /// </summary>
        /// <param name="signUpRequest"></param>
        /// <returns></returns>
        [HttpPost("sign-up")]
        public async Task<UserResponse> SignUp([FromBody] SignUpRequest signUpRequest)
        {
            var parameter = SignUpRequest.ToParameter(signUpRequest);
            var (user, token) = await _userService.SignUpAsync(parameter);

            var result = UserResponse.FromEntity(user, token);
            result.Token = token;
            return result;
        }
    }
}
