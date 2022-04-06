using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeroicBrawlServer.Data.Entities;

namespace HeroicBrawlServer.Data.Repositories.Abstractions
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<ICollection<User>> GetByIds(ICollection<Guid> ids);
    }
}
