using HeroicBrawlServer.Services.Models.Enums;

namespace HeroicBrawlServer.Services.Models.Rooms.Cache
{
    public class PlayerState
    {

        public PlayerState(bool isAlive,
                           string connectionId,
                           int x,
                           int y,
                           int hp,
                           AnimationStateEnum state)
        {
            IsAlive = isAlive;
            ConnectionId = connectionId;
            PositionX = x;
            PositionY = y;
            HP = hp;
            AnimationState = state;
            DeathCount = 0;
            KillCount = 0;
        }

        public bool IsAlive { get; set; }

        public string ConnectionId { get; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public int HP { get; set; }

        public AnimationStateEnum AnimationState { get; set; }

        public int DeathCount { get; set; }

        public int KillCount { get; set; }
    }
}
