using HeroicBrawlServer.Services.Models.Maps;

namespace HeroicBrawlServer.Services.Models.Rooms
{
    public class CreateRoomParameter
    {
        public string Name { get; set; }
        public int Max { get; set; }
        public Map Map { get; set; }
    }
}
