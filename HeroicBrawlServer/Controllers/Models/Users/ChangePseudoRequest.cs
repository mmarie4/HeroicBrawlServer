using HeroicBrawlServer.Services.Models.Users;

namespace HeroicBrawlServer.Controllers.Models.Users
{
    public class ChangePseudoRequest
    {
        public string Pseudo { get; set; }


        /// <summary>
        ///     Builds a ChangePseudoParameter from a ChangePseudoRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static ChangePseudoParameter ToParameter(ChangePseudoRequest request)
        {
            return new ChangePseudoParameter()
            {
                Pseudo = request.Pseudo
            };
        }
    }
}
