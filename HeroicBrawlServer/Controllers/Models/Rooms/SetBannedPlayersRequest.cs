using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeroicBrawlServer.Controllers.Models.Rooms
{
    public class SetBannedPlayersRequest
    {        
        [Required]
        public ICollection<Guid> UserIds { get; set; }
    }
}
