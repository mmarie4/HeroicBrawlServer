using Newtonsoft.Json;

namespace HeroicBrawlServer.Services.Models.Messages.PlayerActions
{
    public class RespawnMessage : BasePlayerActionMessage
    {
        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }
    }
}
