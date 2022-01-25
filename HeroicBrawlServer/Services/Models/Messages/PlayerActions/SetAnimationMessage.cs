using Newtonsoft.Json;

namespace HeroicBrawlServer.Services.Models.Messages.PlayerActions
{
    public class SetAnimationMessage : BasePlayerActionMessage
    {
        [JsonProperty("a")]
        public string AnimationState { get; set; }
    }
}
