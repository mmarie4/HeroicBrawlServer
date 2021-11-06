using HeroicBrawlServer.Services.Models.Users;

namespace HeroicBrawlServer.API.Models.Users
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }


        /// <summary>
        ///     Builds a LoginParameter from a LoginRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static LoginParameter ToParameter(LoginRequest request)
        {
            return new LoginParameter()
            {
                Email = request.Email,
                Password = request.Password
            };
        }
    }
}
