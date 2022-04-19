using System;

namespace HeroicBrawlServer.Services.Models.Rooms.Cache
{
    public class PlayerState
    {

        public PlayerState(string heroName,
                           bool isAlive,
                           string connectionId,
                           int x,
                           int y,
                           int hp,
                           string state)
        {
            HeroName = heroName;
            IsAlive = isAlive;
            ConnectionId = connectionId;
            PositionX = x;
            PositionY = y;
            HP = hp;
            AnimationState = state;
            DeathCount = 0;
            KillCount = 0;
        }

        public string HeroName { get; set; }

        public bool IsAlive { get; set; }

        public string ConnectionId { get; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public int HP { get; set; }

        public string AnimationState { get; set; }

        public int DeathCount { get; set; }

        public int KillCount { get; set; }
    }
}
