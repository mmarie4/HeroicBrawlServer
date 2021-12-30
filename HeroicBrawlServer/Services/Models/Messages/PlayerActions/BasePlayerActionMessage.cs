using System;
namespace HeroicBrawlServer.Services.Models.Messages.PlayerActions
{
    public class BasePlayerActionMessage : BaseMessage
    {
        public Guid RoomId { get; set; }
        public string ConnectionId { get; set;  }
    }
}
