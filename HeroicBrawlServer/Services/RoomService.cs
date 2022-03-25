﻿using HeroicBrawlServer.Services.Abstractions;
using HeroicBrawlServer.Services.Models.Rooms;
using HeroicBrawlServer.Services.Models.Rooms.Cache;
using HeroicBrawlServer.Shared.Models;
using HeroicBrawlServer.Shared.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroicBrawlServer.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUserService _userService;

        private HeroesSettings _heroesSettings;

        public RoomService(IUserService userService)
        {
            _userService = userService;
            _heroesSettings = new HeroesSettings();
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

        public void AddUserToRoom(string connectionId, Guid userId, Guid roomId, Guid heroId, string initialState)
        {
            var room = Cache.Rooms.First(x => x.Id == roomId);

            var spawningPoint = room.Map.GetSpawningPoint();
            room.GameState.Players.Add(new PlayerState(heroId,
                                                       true,
                                                       connectionId,
                                                       spawningPoint.X,
                                                       spawningPoint.Y,
                                                       _heroesSettings.Get(heroId).BaseHp,
                                                       initialState));

            spawningPoint.Update();
        }

        public void RemoveUserFromRoom(string connectionId, Guid userId, Guid roomId)
        {
            var room = Cache.Rooms.First(x => x.Id == roomId);

            var playerStateToRemove = GetPlayerState(roomId, connectionId);
            room.GameState.Players.Remove(playerStateToRemove);
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

            if(playerState.HP <= 0)
            {
                playerState.IsAlive = false;
                playerState.DeathCount++;
                var fromPlayerState = GetPlayerState(roomId, fromConnectionId);
                fromPlayerState.KillCount++;
            }
        }

        public void RespawnPlayer(Guid roomId, string connectionId, int x, int y)
        {
            var playerState = GetPlayerState(roomId, connectionId);

            playerState.PositionX = x;
            playerState.PositionY = y;
            playerState.HP = _heroesSettings.Get(playerState.HeroId).BaseHp;

            Cache.Rooms.First(x => x.Id == roomId)
                       .Map
                       .UpdateSpawningPoint(x, y);
        }

    }
}
