using HeroicBrawlServer.Services.Models.Users;

namespace HeroicBrawlServer.Controllers.Models.Users
{
    public class ChangePasswordRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
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
