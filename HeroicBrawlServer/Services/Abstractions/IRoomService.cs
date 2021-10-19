using System.Threading.Tasks;
using HeroicBrawlServer.DAL.Entities;
using HeroicBrawlServer.Shared.Models;

namespace HeroicBrawlServer.Services.Abstractions
{
    public interface IRoomService
    {
        Task<PaginatedList<Room>> GetPaginatedListAsync(string searchTerm, int limit, int offset);
    }
}
