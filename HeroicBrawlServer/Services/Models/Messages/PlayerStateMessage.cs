using HeroicBrawlServer.Services.Models.Enums;
using HeroicBrawlServer.Services.Models.Rooms.Cache;
using Newtonsoft.Json;

namespace HeroicBrawlServer.Services.Models.Messages
{
    public class PlayerStateMessage : BaseMessage
    {

        public PlayerStateMessage(bool isAlive,
                                  string connectionId,
                                  int x,
                                  int y,
                                  int hp,
                                  string state)
        {
            IsAlive = isAlive;
            ConnectionId = connectionId;
            PositionX = x;
            PositionY = y;
            HP = hp;
            AnimationState = state;
        }

        public PlayerStateMessage(PlayerState playerState)
        {
            ConnectionId = playerState.ConnectionId;
            PositionX = playerState.PositionX;
            PositionY = playerState.PositionY;
            HP = playerState.HP;
            AnimationState = playerState.AnimationState;
        }

        [JsonProperty("a")]
        public bool IsAlive { get; }

        [JsonProperty("c")]
        public string ConnectionId { get; }

        [JsonProperty("x")]
        public int PositionX { get; }

        [JsonProperty("y")]
        public int PositionY { get; }

        [JsonProperty("hp")]
        public int HP { get; }

        [JsonProperty("s")]
        public string AnimationState { get; }
    }
}
