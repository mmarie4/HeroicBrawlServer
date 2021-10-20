using System;
using System.Threading.Tasks;
using HeroicBrawlServer.DAL.Entities;
using HeroicBrawlServer.DAL.Repositories.Abstractions;
using HeroicBrawlServer.Services.Models.Rooms;
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

        public async Task<Room> CreateRoomAsync(CreateRoomParameter parameter, Guid userId)
        {
            var room = new Room()
            {
                Id = Guid.NewGuid(),
                Name = parameter.Name,
                Max = parameter.Max,
                CreatedBy = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedBy = userId,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _roomRepository.AddAsync(room);

            await _roomRepository.SaveAsync();

            return result;
        }
    }
}
