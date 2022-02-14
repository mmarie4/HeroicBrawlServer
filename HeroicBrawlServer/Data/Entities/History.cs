using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeroicBrawlServer.Data.Entities
{
    [Table("histories")]
    public class History : BaseEntity
    {
        public Guid UserId { get; set; }
        public int Rank { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public Guid MapId { get; set; }
        public Guid HeroId { get; set; }
    }
}
