using HeroicBrawlServer.Services.Models.Users;

namespace HeroicBrawlServer.Controllers.Models.Users
{
    public class SignUpRequest
    {
        public string Email { get; set; }
        public string Pseudo { get; set; }
        public string Password { get; set; }

        /// <summary>
        ///     Builds a SignUpParameter from a SignUpRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static SignUpParameter ToParameter(SignUpRequest request)
        {
            return new SignUpParameter()
            {
                Email = request.Email,
                Pseudo = request.Pseudo,
                Password = request.Password
            };
        }
    }
}
