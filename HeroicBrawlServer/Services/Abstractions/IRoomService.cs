using HeroicBrawlServer.Services.Models.Enums;
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
        PaginatedList<Room> GetPaginatedList(string searchTerm, int limit, int offset);

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
        void AddUserToRoom(string connectionId, Guid userId, Guid roomId);

        /// <summary>
        ///     Removes a user from a room
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="userId"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        void RemoveUserFromRoom(string connectionId, Guid userId, Guid roomId);

        /// <summary>
        ///     Returns all room
        /// </summary>
        /// <returns></returns>
        ICollection<Room> Rooms();

        /// <summary>
        ///     Deletes empty rooms
        /// </summary>
        void Clean();

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
        void SetAnimationStatePlayer(Guid roomId, string connectionId, AnimationStateEnum animationState);

        /// <summary>
        ///     Updates player state with new HP
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="connectionId"></param>
        /// <param name="damageTaken"></param>
        void TakeDamagePlayer(Guid roomId, string connectionId, int damageTaken, string fromConnectionId);

        /// <summary>
        ///     Resets player HP and move to spawning point
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="connectionId"></param>
        void RespawnPlayer(Guid roomId, string connectionId);
    }
}
