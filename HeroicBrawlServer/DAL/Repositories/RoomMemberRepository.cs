using System;
using System.Threading.Tasks;
using HeroicBrawlServer.DAL.Entities;
using HeroicBrawlServer.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HeroicBrawlServer.DAL.Repositories
{
    public class RoomMemberRepository : BaseRepository<RoomMember>, IRoomMemberRepository
    {
        public RoomMemberRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<RoomMember> GetByUserIdAndRoomId(Guid userId, Guid roomId)
        {
            return await Entities.FirstOrDefaultAsync(x => x.UserId == userId && x.RoomId == roomId);
        }
    }
}
