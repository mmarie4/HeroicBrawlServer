using System.Threading.Tasks;
using HeroicBrawlServer.Data.Entities;

namespace HeroicBrawlServer.Data.Repositories.Abstractions
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
