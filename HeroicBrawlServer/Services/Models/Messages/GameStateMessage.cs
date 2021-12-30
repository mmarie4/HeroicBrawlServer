using HeroicBrawlServer.Services.Models.Rooms.Cache;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace HeroicBrawlServer.Services.Models.Messages
{
    public class GameStateMessage : BaseMessage
    {

        public GameStateMessage(GameState gameState)
        {
            Players = gameState.Players.Select(x => new PlayerStateMessage(x))
                                       .ToList();
        }

        [JsonProperty("p")]
        public ICollection<PlayerStateMessage> Players { get; }
    }
}
