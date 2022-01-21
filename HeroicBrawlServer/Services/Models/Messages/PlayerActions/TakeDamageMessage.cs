using Newtonsoft.Json;

namespace HeroicBrawlServer.Services.Models.Messages.PlayerActions
{
    public class TakeDamageMessage : BasePlayerActionMessage
    {
        [JsonProperty("d")]
        public int DamageTaken { get; set; }
        [JsonProperty("f")]
        public string FromConnectionId { get; set; }
    }
}
