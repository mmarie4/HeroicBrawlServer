using HeroicBrawlServer.Data.Entities;
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
        private readonly ISettingsService _settingsService;

        public RoomService(IUserService userService,
                           ISettingsService settingsService)
        {
            _userService = userService;
            _settingsService = settingsService;
        }

        public PaginatedList<Room> GetPaginatedList(string? searchTerm, int limit, int offset)
        {
            var total = Cache.Rooms.Count;
            List<Room> rooms;
            if (searchTerm != null && !string.IsNullOrWhiteSpace(searchTerm))
            {
                rooms = Cache.Rooms.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()))
                                   .Take(limit)
                                   .Skip(offset)
                                   .ToList();
            }
            else
            {
                rooms = Cache.Rooms.Take(limit)
                              .Skip(offset)
                              .ToList();
            }

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
            var room = new Room(parameter.Name, parameter.Max, parameter.Map, user);
            Cache.AddRoom(room);

            return room;
        }

        public void AddUserToRoom(string connectionId, Guid userId, Guid roomId, string heroName, string initialState)
        {
            var room = GetRoom(roomId);

            var spawningPoint = room.Map.GetSpawningPoint();
            room.GameState.Players.Add(new PlayerState(heroName,
                                                       true,
                                                       connectionId,
                                                       spawningPoint.X,
                                                       spawningPoint.Y,
                                                       _settingsService.GetHeroSettings(heroName).BaseHp,
                                                       initialState));

            spawningPoint.Update();
        }

        public void RemoveUserFromRoom(string connectionId, Guid userId, Guid roomId)
        {
            var room = GetRoom(roomId);

            var playerStateToRemove = GetPlayerState(roomId, connectionId);
            room.GameState.Players.Remove(playerStateToRemove);
        }

        public async Task<PaginatedList<User>> GetPaginatedBannedPlayers(Guid roomId, int limit, int offset)
        {
            var room = GetRoom(roomId);

            var userIds = room.BannedPlayers.Take(limit)
                                            .Skip(offset)
                                            .ToList();

            return new PaginatedList<User>()
            {
                Values = await _userService.GetUsersByIds(userIds),
                Limit = limit,
                Offset = offset,
                Total = room.BannedPlayers.Count
            };
        }

        public async Task UpdateBannedPlayerList(Guid roomId, ICollection<Guid> userIds)
        {
            var room = GetRoom(roomId);
            room.BannedPlayers = userIds;
        }

        public void RemovePlayerFromRooms(string connectionId)
        {
            foreach (var room in Cache.Rooms)
            {
                room.GameState.Players.RemoveAll(p => p.ConnectionId == connectionId);
            }
        }

        private PlayerState GetPlayerState(Guid roomId, string connectionId)
        {
            return Cache.Rooms.First(x => x.Id == roomId)
                         .GameState
                         .Players
                         .First(x => x.ConnectionId == connectionId);
        }

        public void MovePlayer(Guid roomId, string connectionId, int positionX, int positionY)
        {
            var playerState = GetPlayerState(roomId, connectionId);

            playerState.PositionX = positionX;
            playerState.PositionY = positionY;

        }

        public void SetAnimationStatePlayer(Guid roomId, string connectionId, string animationState)
        {
            var playerState = GetPlayerState(roomId, connectionId);

            playerState.AnimationState = animationState;
        }

        public void TakeDamagePlayer(Guid roomId, string connectionId, int damageTaken, string fromConnectionId)
        {
            var playerState = GetPlayerState(roomId, connectionId);
            playerState.HP = playerState.HP - damageTaken;
        }

        public void Die(Guid roomId, string connectionId, string fromConnectionId)
        {
            var playerState = GetPlayerState(roomId, connectionId);

            playerState.IsAlive = false;
            playerState.DeathCount++;
            var fromPlayerState = GetPlayerState(roomId, fromConnectionId);
            fromPlayerState.KillCount++;
        }

        public void RespawnPlayer(Guid roomId, string connectionId, int x, int y)
        {
            var playerState = GetPlayerState(roomId, connectionId);

            playerState.PositionX = x;
            playerState.PositionY = y;
            playerState.HP = _settingsService.GetHeroSettings(playerState.HeroName).BaseHp;

            Cache.Rooms.First(x => x.Id == roomId)
                       .Map
                       .UpdateSpawningPoint(x, y);
        }

        private Room GetRoom(Guid roomId)
        {
            var room = Cache.Rooms.FirstOrDefault(x => x.Id == roomId);
            if (room == null)
            {
                throw new Exception($"Room {roomId} not found");
            }
            return room;
        }

    }
}
