using HeroicBrawlServer.Services.Models.Maps;
using HeroicBrawlServer.Services.Models.Rooms.Cache;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace HeroicBrawlServer.Services.Models.Messages
{
    public class GameStateMessage : BaseMessage
    {

        public GameStateMessage(GameState gameState, Map map)
        {
            Players = gameState.Players.Select(x => new PlayerStateMessage(x))
                                       .ToList();
            SpawningPoints = map.SpawningPoints.Select(x => new MapSpawningPointMessage(x))
                                               .ToList();
        }

        [JsonProperty("p")]
        public ICollection<PlayerStateMessage> Players { get; }

        [JsonProperty("sp")]
        public ICollection<MapSpawningPointMessage> SpawningPoints { get; }
    }
}
