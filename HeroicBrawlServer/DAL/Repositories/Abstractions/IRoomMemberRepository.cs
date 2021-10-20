using System;
using System.Threading.Tasks;
using HeroicBrawlServer.DAL.Entities;

namespace HeroicBrawlServer.DAL.Repositories.Abstractions
{
    public interface IRoomMemberRepository : IBaseRepository<RoomMember>
    {
        Task<RoomMember> GetByUserIdAndRoomId(Guid userId, Guid roomId);
    }
}
