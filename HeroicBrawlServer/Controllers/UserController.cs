using HeroicBrawlServer.Controllers.Models.Users;
using HeroicBrawlServer.Services.Abstractions;
using HeroicBrawlServer.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HeroicBrawlServer.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
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

        [HttpPut("change-pseudo")]
        [ProducesResponseType(204)]
        [Authorize]
        public async Task<IActionResult> ChangePseudo([FromBody] ChangePseudoRequest changePseudoRequest)
        {
            var userId = HttpContext.User.ExtractUserId();

            var parameter = ChangePseudoRequest.ToParameter(changePseudoRequest);

            var _ = await _userService.ChangePseudo(parameter, userId);

            return NoContent();
        }

        [HttpPut("change-password")]
        [ProducesResponseType(204)]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            if (changePasswordRequest.NewPassword != changePasswordRequest.NewPassword2)
            {
                throw new Exception("Passwords don't match");
            }

            var userId = HttpContext.User.ExtractUserId();

            var parameter = ChangePasswordRequest.ToParameter(changePasswordRequest);

            var _ = await _userService.ChangePassword(parameter, userId);

            return NoContent();
        }
    }
}
