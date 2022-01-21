using Newtonsoft.Json;
using System;
namespace HeroicBrawlServer.Services.Models.Messages.PlayerActions
{
    public class BasePlayerActionMessage : BaseMessage
    {
        [JsonProperty("r")]
        public Guid RoomId { get; set; }
        [JsonProperty("c")]
        public string ConnectionId { get; set;  }
    }
}
