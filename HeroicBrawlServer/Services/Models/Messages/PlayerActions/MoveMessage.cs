using System;
using HeroicBrawlServer.Services.Models.Enums;
using Newtonsoft.Json;

namespace HeroicBrawlServer.Services.Models.Messages.PlayerActions
{
    public class MoveMessage : BasePlayerActionMessage
    {
        [JsonProperty("x")]
        public int PositionX { get; }
        [JsonProperty("y")]
        public int PositionY { get; }
        [JsonProperty("d")]
        public DirectionEnum Direction { get; }
    }
}
