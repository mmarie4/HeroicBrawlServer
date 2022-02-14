using System.Threading.Tasks;
using HeroicBrawlServer.Data.Entities;
using HeroicBrawlServer.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HeroicBrawlServer.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await Entities.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
