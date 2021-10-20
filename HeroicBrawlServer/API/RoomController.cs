using System;
using System.Threading.Tasks;
using HeroicBrawlServer.API.Models.Rooms;
using HeroicBrawlServer.Services.Abstractions;
using HeroicBrawlServer.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace HeroicBrawlServer.API
{

    [Route("api/rooms")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        /// <summary>
        ///     Get paginated list of rooms with or without search term
        /// </summary>
        /// <param name="request">Search request</param>
        /// <returns></returns>
        [HttpPost("search")]
        [ProducesResponseType(typeof(PaginatedList<RoomResponse>), 200)]
        public async Task<PaginatedList<RoomResponse>> GetPaginatedList([FromBody] SearchRoomRequest request)
        {
            var rooms = await _roomService.GetPaginatedListAsync(request.SearchTerm, request.Limit, request.Offset);

            return RoomResponse.FromEntities(rooms);
        }

        /// <summary>
        ///     Creates a room
        /// </summary>
        /// <param name="request">Create room request</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(RoomResponse), 200)]
        public async Task<RoomResponse> CreateRoom([FromBody] CreateRoomRequest request)
        {
            // TODO: use token to get userId
            var userId = Guid.NewGuid();

            var parameter = CreateRoomRequest.ToParameter(request);

            var room = await _roomService.CreateRoomAsync(parameter, userId);

            return RoomResponse.FromEntity(room);
        }
    }
}
