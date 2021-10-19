using System.Threading.Tasks;
using HeroicBrawlServer.DAL.Entities;
using HeroicBrawlServer.DAL.Repositories.Abstractions;
using HeroicBrawlServer.Shared.Models;

namespace HeroicBrawlServer.Services
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<PaginatedList<Room>> GetPaginatedListAsync(string searchTerm, int limit, int offset)
        {
            var rooms = await _roomRepository.SearchRoomsAsync(searchTerm, limit, offset);
            var total = await _roomRepository.GetTotalCountAsync(searchTerm);

            return new PaginatedList<Room>()
            {
                Values = rooms,
                Limit = limit,
                Offset = offset,
                Total = total
            };
        }
    }
}
