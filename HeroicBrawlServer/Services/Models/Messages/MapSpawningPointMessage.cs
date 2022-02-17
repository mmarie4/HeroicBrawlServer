using HeroicBrawlServer.Services.Models.Maps;
using Newtonsoft.Json;
using System;

namespace HeroicBrawlServer.Services.Models.Messages
{
    public class MapSpawningPointMessage
    {

        public MapSpawningPointMessage(MapSpawningPoint spawningPoint)
        {
            X = spawningPoint.X;
            Y = spawningPoint.Y;
            LastSpawnDate = spawningPoint.LastSpawnDate;
        }

        [JsonProperty("x")]
        public int X { get; }

        [JsonProperty("y")]
        public int Y { get; }

        [JsonProperty("d")]
        public DateTime LastSpawnDate { get; }
    }
}
