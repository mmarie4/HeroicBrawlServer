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
    }
}
