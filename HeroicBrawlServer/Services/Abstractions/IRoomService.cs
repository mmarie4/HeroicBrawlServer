using HeroicBrawlServer.Data.Entities;
using HeroicBrawlServer.Services.Models.Rooms;
using HeroicBrawlServer.Services.Models.Rooms.Cache;
using HeroicBrawlServer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeroicBrawlServer.Services.Abstractions
{
    public interface IRoomService
    {
        /// <summary>
        ///     Returns paginated list of rooms filtered by search term
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        PaginatedList<Room> GetPaginatedList(string? searchTerm, int limit, int offset);

        /// <summary>
        ///     Creates a new room and adds the creator inside
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Room> CreateRoom(CreateRoomParameter parameter, Guid userId);

        /// <summary>
        ///     Add a user to a room
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="userId"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        void AddUserToRoom(string connectionId, Guid userId, Guid roomId, Guid heroId, string initialState);

        /// <summary>
        ///     Removes a user from a room
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="userId"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        void RemoveUserFromRoom(string connectionId, Guid userId, Guid roomId);

        /// <summary>
        ///     Returns paginated list of banned players of a room
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        Task<PaginatedList<User>> GetPaginatedBannedPlayers(Guid roomId, int limit, int offset);

        /// <summary>
        ///     Updates the list of banned players of a room
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        Task UpdateBannedPlayerList(Guid roomId, ICollection<Guid> userIds);

        /// <summary>
        ///     Remove players from all rooms where he was connected
        /// </summary>
        /// <param name="connectionId"></param>
        void RemovePlayerFromRooms(string connectionId);

        /// <summary>
        ///     Updates player state with new position
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="connectionId"></param>
        /// <param name="positionX"></param>
        /// <param name="positionY"></param>
        void MovePlayer(Guid roomId, string connectionId, int positionX, int positionY);

        /// <summary>
        ///     Updates player state with new animation state
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="connectionId"></param>
        /// <param name="animationState"></param>
        void SetAnimationStatePlayer(Guid roomId, string connectionId, string animationState);

        /// <summary>
        ///     Updates player state with new HP
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="connectionId"></param>
        /// <param name="damageTaken"></param>
        void TakeDamagePlayer(Guid roomId, string connectionId, int damageTaken, string fromConnectionId);

        /// <summary>
        ///     Updates player states death and kill counts
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="connectionId"></param>
        /// <param name="fromConnectionId"></param>
        void Die(Guid roomId, string connectionId, string fromConnectionId);

        /// <summary>
        ///     Resets player HP and move to spawning point
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="connectionId"></param>
        void RespawnPlayer(Guid roomId, string connectionId, int x, int y);
    }
}
