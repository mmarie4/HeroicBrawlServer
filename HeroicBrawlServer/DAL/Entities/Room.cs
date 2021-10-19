using System;
namespace HeroicBrawlServer.DAL.Entities
{
    public class Room : BaseEntity
    {
        public string Name { get; set; }
        public int Max { get; set; }
    }
}
