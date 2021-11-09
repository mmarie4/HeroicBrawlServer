using Newtonsoft.Json;

namespace HeroicBrawlServer.Services.Models.Messages
{
    public class PlayerStateMessage : BaseMessage
    {

        public PlayerStateMessage(int x,
                                  int y,
                                  int hp,
                                  string state)
        {
            PositionX = x;
            PositionY = y;
            HP = hp;
            State = state;
        }

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
