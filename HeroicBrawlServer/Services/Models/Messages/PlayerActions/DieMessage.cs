using Newtonsoft.Json;

namespace HeroicBrawlServer.Services.Models.Messages.PlayerActions
{
    public class DieMessage : BasePlayerActionMessage
    {
        [JsonProperty("f")]
        public string FromConnectionId { get; set; }
    }
}
