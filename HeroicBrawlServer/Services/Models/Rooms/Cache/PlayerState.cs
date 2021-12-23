namespace HeroicBrawlServer.Services.Models.Rooms.Cache
{
    public class PlayerState
    {

        public PlayerState(string connectionId,
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

        public string ConnectionId { get; }

        public int PositionX { get; }

        public int PositionY { get; }

        public int HP { get; }

        public string State { get; }
    }
}
