using System.Threading.Tasks;
using HeroicBrawlServer.Controllers.Middleware;
using HeroicBrawlServer.Controllers.Models.Rooms;
using HeroicBrawlServer.Services.Abstractions;
using HeroicBrawlServer.Shared.Extensions;
using HeroicBrawlServer.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeroicBrawlServer.Controllers
{
    [ApiController]
    [Route("api/rooms")]
    [Authorize]
    [ExceptionFilter]
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
    }
}
