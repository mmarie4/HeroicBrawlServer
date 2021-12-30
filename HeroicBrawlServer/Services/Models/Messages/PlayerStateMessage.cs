using HeroicBrawlServer.Services.Models.Enums;
using HeroicBrawlServer.Services.Models.Rooms.Cache;
using Newtonsoft.Json;

namespace HeroicBrawlServer.Services.Models.Messages
{
    public class PlayerStateMessage : BaseMessage
    {

        public PlayerStateMessage(string connectionId,
                                  int x,
                                  int y,
                                  int hp,
                                  AnimationStateEnum state)
        {
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

        [JsonProperty("c")]
        public string ConnectionId { get; }

        [JsonProperty("x")]
        public int PositionX { get; }

        [JsonProperty("y")]
        public int PositionY { get; }

        [JsonProperty("hp")]
        public int HP { get; }

        [JsonProperty("s")]
        public AnimationStateEnum AnimationState { get; }
    }
}
