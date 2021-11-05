using System;

namespace HeroicBrawlServer.Services.Models.Rooms
{
    public class CreateRoomParameter
    {
        public string Name { get; set; }
        public int Max { get; set; }
        public Guid MapId { get; set; }
    }
}
