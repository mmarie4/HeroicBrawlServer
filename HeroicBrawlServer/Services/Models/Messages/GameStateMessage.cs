using Newtonsoft.Json;
using System.Collections.Generic;

namespace HeroicBrawlServer.Services.Models.Messages
{
    public class GameStateMessage : BaseMessage
    {

        public GameStateMessage()
        {
            Players = new List<PlayerStateMessage>();
        }

        [JsonProperty("p")]
        public ICollection<PlayerStateMessage> Players { get; }
    }
}
