using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeroicBrawlServer.DAL.Entities
{
    [Table("histories")]
    public class History : BaseEntity
    {
        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("rank")]
        public int Rank { get; set; }

        [Column("kills")]
        public int Kills { get; set; }

        [Column("deaths")]
        public int Deaths { get; set; }

        [Column("map_id")]
        public Guid MapId { get; set; }

        [Column("hero_id")]
        public Guid HeroId { get; set; }
    }
}
