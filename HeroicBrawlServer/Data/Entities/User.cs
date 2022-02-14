using System.ComponentModel.DataAnnotations.Schema;

namespace HeroicBrawlServer.Data.Entities
{
    [Table("users")]
    public class User : BaseEntity
    {
        public string Pseudo { get; set; }
        public string Email { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
    }
}
