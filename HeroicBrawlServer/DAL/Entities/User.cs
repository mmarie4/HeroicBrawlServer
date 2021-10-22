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
    }
}
