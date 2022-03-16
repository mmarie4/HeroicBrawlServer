using System.Collections.Generic;
using System.Linq;

namespace HeroicBrawlServer.Services.Models.Rooms.Cache;
    public static class Cache
    {
        private static ICollection<Room> PrivateRooms = new List<Room>();

        public static ICollection<Room> Rooms
        {
            get => PrivateRooms.ToList();
        }

        public static void AddRoom(Room room)
        {
            PrivateRooms.Add(room);
        }
    
        public static void RemoveRoom(Room room)
        {
            PrivateRooms.Remove(room);
        }

    }
