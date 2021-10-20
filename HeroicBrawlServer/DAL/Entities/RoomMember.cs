using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeroicBrawlServer.DAL.Entities
{
    [Table("room_members")]
    public class RoomMember : BaseEntity
    {
        [Column("room_id")]
        public Guid RoomId { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }
    }
}
