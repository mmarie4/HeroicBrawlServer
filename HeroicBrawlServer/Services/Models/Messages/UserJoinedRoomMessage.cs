using System;

namespace HeroicBrawlServer.Services.Models.Messages
{

    /// <summary>
    ///     Message sent to connected users when a user joined a room
    /// </summary>
    public class UserJoinedRoomMessage : BaseMessage
    {

        /// <summary>
        ///     Constructor for UserJoinedRoomMessage
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pseudo"></param>
        /// <param name="heroId"></param>
        public UserJoinedRoomMessage(Guid userId, string pseudo, Guid heroId)
        {
            UserId = userId;
            Pseudo = pseudo;
            HeroId = heroId;
        }

        /// <summary>
        ///     ID of the user joining the room
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        ///     User's pseudo
        /// </summary>
        public string Pseudo { get; }

        /// <summary>
        ///     ID of the hero the user is playing
        /// </summary>
        public Guid HeroId { get; }
    }
}
