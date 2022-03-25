using HeroicBrawlServer.Services.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace HeroicBrawlServer.Controllers.Models.Users
{
    public class ChangePasswordRequest
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string NewPassword2 { get; set; }


        /// <summary>
        ///     Builds a ChangePasswordParameter from a ChangePasswordRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static ChangePasswordParameter ToParameter(ChangePasswordRequest request)
        {
            return new ChangePasswordParameter()
            {
                OldPassword = request.OldPassword,
                NewPassword = request.NewPassword
            };
        }
    }
}
