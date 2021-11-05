using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroicBrawlServer.DAL.Entities;
using HeroicBrawlServer.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HeroicBrawlServer.DAL.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(AppDbContext context) : base(context)
        {
        }


        /// <summary>
        ///     Search rooms by name
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<ICollection<Room>> SearchRoomsAsync(string searchTerm, int limit, int offset)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                return await Entities
                                .Include(x => x.CreatedBy)
                                .Include(x => x.UpdatedBy)
                                .Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()))
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
            }

            return await Entities
                             .Include(x => x.CreatedBy)
                             .Include(x => x.UpdatedBy)
                             .Skip(offset)
                             .Take(limit)
                             .ToListAsync();
        }

        /// <summary>
        ///     Get total count of rooms by name
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                return await Entities
                                .Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()))
                                .CountAsync();
            }

            return await Entities.CountAsync();
        }
    }
}
