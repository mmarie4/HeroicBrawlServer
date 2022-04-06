using HeroicBrawlServer.Controllers.Models.Rooms;
using HeroicBrawlServer.Controllers.Models.Users;
using HeroicBrawlServer.Services.Abstractions;
using HeroicBrawlServer.Shared.Extensions;
using HeroicBrawlServer.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HeroicBrawlServer.Controllers
{
    [ApiController]
    [Route("api/rooms")]
    [Authorize]
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
        public PaginatedList<RoomResponse> GetPaginatedList([FromBody] SearchRoomRequest request)
        {
            var rooms = _roomService.GetPaginatedList(request.SearchTerm, request.Limit, request.Offset);

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
            var userId = HttpContext.User.ExtractUserId();

            var parameter = CreateRoomRequest.ToParameter(request);

            var room = await _roomService.CreateRoom(parameter, userId);

            return RoomResponse.FromEntity(room);
        }

        /// <summary>
        ///     Get paginated list of banned players
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{roomId}/banned-players")]
        [ProducesResponseType(typeof(PaginatedList<BannedPlayerResponse>), 200)]
        public async Task<PaginatedList<BannedPlayerResponse>> GetBannedPlayers([FromRoute] Guid roomId, [FromBody] GetBannedPlayersRequest request)
        {
            var players = await _roomService.GetPaginatedBannedPlayers(roomId, request.Limit, request.Offset);

            return BannedPlayerResponse.FromEntities(players);
        }

        /// <summary>
        ///     Sets the list of banned players of a room
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{roomId}/banned-players")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> SetBannedPlayers([FromRoute] Guid roomId, [FromBody] SetBannedPlayersRequest request)
        {
            var players = _roomService.UpdateBannedPlayerList(roomId, request.UserIds);

            return NoContent();
        }


    }
}
