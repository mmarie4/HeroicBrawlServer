using HeroicBrawlServer.Services.Models.Enums;
using Newtonsoft.Json;

namespace HeroicBrawlServer.Services.Models.Messages.PlayerActions
{
    public class SetAnimationMessage : BasePlayerActionMessage
    {
        [JsonProperty("a")]
        public AnimationStateEnum AnimationState { get; set; }
    }
}
