using HeroicBrawlServer.Services.Abstractions;
using HeroicBrawlServer.Services.Models.Rooms;
using HeroicBrawlServer.Services.Models.Rooms.Cache;
using HeroicBrawlServer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroicBrawlServer.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUserService _userService;

        private ICollection<Room> _rooms;
        private TimeSpan _emptyRoomTTL;

        public RoomService(IUserService userService)
        {
            _userService = userService;

            _rooms = new List<Room>();
            _emptyRoomTTL = new TimeSpan(0, 10, 0);
        }

        public PaginatedList<Room> GetPaginatedList(string searchTerm, int limit, int offset)
        {
            var total = _rooms.Count;
            var rooms = _rooms.Take(limit)
                              .Skip(offset)
                              .ToList();

            return new PaginatedList<Room>()
            {
                Values = rooms,
                Limit = limit,
                Offset = offset,
                Total = total
            };
        }

        public async Task<Room> CreateRoom(CreateRoomParameter parameter, Guid userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            var room = new Room(parameter.Name, parameter.Max, parameter.MapId, user);
            _rooms.Add(room);

            return room;
        }

        public void AddUserToRoom(string connectionId, Guid userId, Guid roomId)
        {
            var room = _rooms.First(x => x.Id == roomId);
            room.Users.Add(new OnlineUser(connectionId, userId));
        }

        public void RemoveUserFromRoom(string connectionId, Guid userId, Guid roomId)
        {
            var room = _rooms.First(x => x.Id == roomId);
            var user = room.Users.First(x => x.ConnectionId == connectionId);
            room.Users.Remove(user);
        }

        public ICollection<Room> Rooms()
        {
            return _rooms;
        }

        public void Clean()
        {
            foreach (var room in _rooms)
            {
                if (!room.Users.Any() && (DateTime.UtcNow - room.CreatedAt) > _emptyRoomTTL)
                {
                    _rooms.Remove(room);
                };
            }
        }
    }
}
