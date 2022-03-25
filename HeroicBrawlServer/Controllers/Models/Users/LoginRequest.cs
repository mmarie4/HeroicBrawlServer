using HeroicBrawlServer.Services.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace HeroicBrawlServer.Controllers.Models.Users
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
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
