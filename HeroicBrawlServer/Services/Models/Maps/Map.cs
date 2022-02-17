using System;
using System.Collections.Generic;
using System.Linq;

namespace HeroicBrawlServer.Services.Models.Maps
{
    public class Map
    {
        public Map()
        {
            SpawningPoints = new List<MapSpawningPoint>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<MapSpawningPoint> SpawningPoints { get; }

        public MapSpawningPoint GetSpawningPoint()
        {
            var spawningPoint = SpawningPoints.OrderByDescending(x => x.LastSpawnDate)
                                              .FirstOrDefault();

            return spawningPoint ?? new MapSpawningPoint(0, 0);
        }

        public void UpdateSpawningPoint(int x, int y)
        {
            var spawningPoint = SpawningPoints.FirstOrDefault(p => p.X == x && p.Y == y);

            if (spawningPoint != null)
            {
                spawningPoint.Update();
            }
        }
    }
}
