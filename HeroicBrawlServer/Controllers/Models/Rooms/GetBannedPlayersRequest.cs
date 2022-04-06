using System.ComponentModel.DataAnnotations;

namespace HeroicBrawlServer.Controllers.Models.Rooms
{
    public class GetBannedPlayersRequest
    {        
        [Required]
        public int Limit { get; set; }
        
        [Required]
        public int Offset { get; set; }
    }
}
