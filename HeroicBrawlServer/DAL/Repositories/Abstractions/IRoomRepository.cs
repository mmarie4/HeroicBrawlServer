using System.Collections.Generic;
using System.Threading.Tasks;
using HeroicBrawlServer.DAL.Entities;

namespace HeroicBrawlServer.DAL.Repositories.Abstractions
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        Task<ICollection<Room>> SearchRoomsAsync(string searchTerm, int limit, int offset);
        Task<int> GetTotalCountAsync(string searchTerm);
    }
}
