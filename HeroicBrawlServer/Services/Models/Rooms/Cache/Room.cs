using HeroicBrawlServer.DAL.Entities;
using HeroicBrawlServer.Services.Models.Maps;
using System;
using System.Collections.Generic;

namespace HeroicBrawlServer.Services.Models.Rooms.Cache
{
    public class Room
    {
        public Guid Id { get; }

        public string Name { get; set; }

        public int Max { get; }

        public Map Map { get; set; }

        public DateTime CreatedAt { get; }

        public User CreatedBy { get; }

        public GameState GameState { get; }

        public Room(string name, int max, Map map, User createdBy)
        {
            Id = Guid.NewGuid();
            Name = name;
            Max = max;
            Map = map;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
            GameState = new GameState();
        }
    }
}
