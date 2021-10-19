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

        [HttpPost("search")]
        [ProducesResponseType(typeof(PaginatedList<RoomResponse>), 200)]
        public async Task<PaginatedList<RoomResponse>> GetPaginatedList([FromBody] SearchRoomRequest request)
        {
            var rooms = await _roomService.GetPaginatedListAsync(request.SearchTerm, request.Limit, request.Offset);

            return RoomResponse.FromEntities(rooms);
        }
    }
}
