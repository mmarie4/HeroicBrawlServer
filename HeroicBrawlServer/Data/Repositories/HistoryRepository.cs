using HeroicBrawlServer.Data.Entities;
using HeroicBrawlServer.Data.Repositories.Abstractions;

namespace HeroicBrawlServer.Data.Repositories
{
    public class HistoryRepository : BaseRepository<History>, IHistoryRepository
    {
        public HistoryRepository(AppDbContext context) : base(context) { }
    }
}
