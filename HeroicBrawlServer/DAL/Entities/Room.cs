using System.ComponentModel.DataAnnotations.Schema;

namespace HeroicBrawlServer.DAL.Entities
{
    [Table("rooms")]
    public class Room : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("max")]
        public int Max { get; set; }
    }
}
