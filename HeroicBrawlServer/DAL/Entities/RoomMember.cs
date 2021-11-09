using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeroicBrawlServer.DAL.Entities
{
    [Table("room_members")]
    public class RoomMember : BaseEntity
    {
        [Column("room_id")]
        [ForeignKey("rooms")]
        public Guid RoomId { get; set; }

        [Column("user_id")]
        [ForeignKey("rooms")]
        public Guid UserId { get; set; }

        public Guid ConnectionId { get; set; }
    }
}
