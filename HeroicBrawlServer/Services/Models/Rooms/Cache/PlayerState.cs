using HeroicBrawlServer.Services.Models.Enums;

namespace HeroicBrawlServer.Services.Models.Rooms.Cache
{
    public class PlayerState
    {

        public PlayerState(string connectionId,
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

        public string ConnectionId { get; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public int HP { get; set; }

        public AnimationStateEnum AnimationState { get; set; }
    }
}
