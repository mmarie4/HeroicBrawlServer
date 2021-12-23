using System.Collections.Generic;

namespace HeroicBrawlServer.Services.Models.Rooms.Cache
{
    public class GameState
    {
        public ICollection<PlayerState> Players { get; }

        public GameState()
        {
            Players = new List<PlayerState>();
        }
    }
}
