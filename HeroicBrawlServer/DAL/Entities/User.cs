using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeroicBrawlServer.DAL.Entities
{
    [Table("users")]
    public class User : BaseEntity
    {
        [Column("pseudo")]
        public string Pseudo { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password_salt")]
        public string PasswordSalt { get; set; }

        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [Column("created_by")]
        public Guid CreatedById { get; set; }

        [Column("updated_by")]
        public Guid UpdatedById { get; set; }

        [ForeignKey("created_by")]
        public ICollection<BaseEntity> EntitiesCreated { get; set; }
        
        [ForeignKey("base_entity")]
        public ICollection<BaseEntity> EntitiesUpdated { get; set; }

        [ForeignKey("histories")]
        public ICollection<History> Histories { get; set; }
    }
}
