using System;

namespace HeroicBrawlServer.Services.Models.Rooms.Cache
{
    public class OnlineUser
    {
        public string ConnectionId { get; }
        public Guid UserId { get; }


        public OnlineUser(string connectionId, Guid userId)
        {
            ConnectionId = connectionId;
            UserId = userId;
        }
    }
}
