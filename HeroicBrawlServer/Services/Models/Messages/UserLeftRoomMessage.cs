using System;

namespace HeroicBrawlServer.Services.Models.Messages
{

    /// <summary>
    ///     Message sent to connected users when a user joined a room
    /// </summary>
    public class UserLeftRoomMessage : BaseMessage
    {

        /// <summary>
        ///     Constructor for UserLeftRoomMessage
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="connectionId"></param>
        public UserLeftRoomMessage(Guid userId, Guid connectionId)
        {
            UserId = userId;
            ConnectionId = connectionId;
        }

        /// <summary>
        ///     ID of the user joining the room
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        ///     ID of the connection
        /// </summary>
        public Guid ConnectionId { get; }
    }
}
