using System.Threading.Tasks;
using HeroicBrawlServer.DAL.Entities;

namespace HeroicBrawlServer.DAL.Repositories.Abstractions
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
