using HeroicBrawlServer.Services.Models.Rooms.Cache;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HeroicBrawlServer.Services.Models.Messages
{
    public class GameStateMessage : BaseMessage
    {

        public GameStateMessage(GameState gameState)
        {
            Players = new List<PlayerStateMessage>();
            
            // TODO: map all gameState.Players to new PlayerStateMessage and add it in Players
        }

        [JsonProperty("p")]
        public ICollection<PlayerStateMessage> Players { get; }
    }
}
