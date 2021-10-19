using System.Collections.Generic;

namespace HeroicBrawlServer.Shared.Models
{
    public class PaginatedList<T>
    {
        public ICollection<T> Values { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Total { get; set; }
    }
}
