using System;

namespace HeroicBrawlServer.Services.Models.Maps
{
    public class MapSpawningPoint
    {
        public MapSpawningPoint(int x, int y)
        {
            X = x;
            Y = y;
            LastSpawnDate = DateTime.Now;
        }

        public int X { get; }
        public int Y { get; }
        public DateTime LastSpawnDate { get; set; }

        public void Update()
        {
            LastSpawnDate = DateTime.UtcNow;
        }
    }
}
