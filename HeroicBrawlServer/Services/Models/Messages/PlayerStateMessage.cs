﻿using Newtonsoft.Json;

namespace HeroicBrawlServer.Services.Models.Messages
{
    public class PlayerStateMessage : BaseMessage
    {

        public PlayerStateMessage(string connectionId,
                                  int x,
                                  int y,
                                  int hp,
                                  string state)
        {
            ConnectionId = connectionId;
            PositionX = x;
            PositionY = y;
            HP = hp;
            State = state;
        }

        [JsonProperty("c")]
        public string ConnectionId { get; }

        [JsonProperty("x")]
        public int PositionX { get; }

        [JsonProperty("y")]
        public int PositionY { get; }

        [JsonProperty("hp")]
        public int HP { get; }

        [JsonProperty("s")]
        public string State { get; }
    }
}
