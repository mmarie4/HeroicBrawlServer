using System;
using System.Threading.Tasks;
using HeroicBrawlServer.DAL.Entities;
using HeroicBrawlServer.Services.Models.Rooms;
using HeroicBrawlServer.Shared.Models;

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
        Task<PaginatedList<Room>> GetPaginatedListAsync(string searchTerm, int limit, int offset);

        /// <summary>
        ///     Creates a new room and adds the creator inside
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Room> CreateRoomAsync(CreateRoomParameter parameter, Guid userId);

        /// <summary>
        ///     Add a user to a room
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        Task AddMemberToRoomAsync(Guid userId, Guid roomId);

        /// <summary>
        ///     Removes a user from a room
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        Task RemoveMemberFromRoomAsync(Guid userId, Guid roomId)
    }
}
