using HeroicBrawlServer.DAL.Entities;
using System;
using System.Collections.Generic;

namespace HeroicBrawlServer.Services.Models.Rooms.Cache
{
    public class Room
    {
        public Guid Id { get; }

        public string Name { get; set; }

        public int Max { get; }

        public Guid MapId { get; }

        public DateTime CreatedAt { get; }

        public User CreatedBy { get; }

        public ICollection<OnlineUser> Users { get; }

        public GameState GameState { get; }

        public Room(string name, int max, Guid mapId, User createdBy)
        {
            Id = Guid.NewGuid();
            Name = name;
            Max = max;
            MapId = mapId;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
            Users = new List<OnlineUser>();
            GameState = new GameState();
        }
    }
}
