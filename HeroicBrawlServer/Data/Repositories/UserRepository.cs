using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ICollection<User>> GetByIds(ICollection<Guid> ids)
        {
            return await Entities.Where(x => ids.Any(id => id == x.Id)).ToListAsync();
        }
    }
}
