using System;
using System.Threading.Tasks;
using HeroicBrawlServer.DAL.Entities;
using HeroicBrawlServer.DAL.Repositories.Abstractions;
using HeroicBrawlServer.Services.Abstractions;
using HeroicBrawlServer.Services.Models.Rooms;
using HeroicBrawlServer.Shared.Models;

namespace HeroicBrawlServer.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomMemberRepository _roomMemberRepository;

        public RoomService(IRoomRepository roomRepository, IRoomMemberRepository roomMemberRepository)
        {
            _roomRepository = roomRepository;
            _roomMemberRepository = roomMemberRepository;
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
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedById = userId,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _roomRepository.AddAsync(room);

            await _roomRepository.SaveAsync();

            return result;
        }

        public async Task AddMemberToRoomAsync(Guid userId, Guid roomId)
        {
            var roomMember = new RoomMember()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                RoomId = roomId,
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedById = userId,
                UpdatedAt = DateTime.UtcNow
            };

            await _roomMemberRepository.AddAsync(roomMember);
        }

        public async Task RemoveMemberFromRoomAsync(Guid userId, Guid roomId)
        {
            var roomMember = await _roomMemberRepository.GetByUserIdAndRoomId(userId, roomId);

            if (roomMember != null)
                await _roomMemberRepository.Remove(roomMember);
        }
    }
}
