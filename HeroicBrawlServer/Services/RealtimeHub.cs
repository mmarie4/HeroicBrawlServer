using HeroicBrawlServer.Services.Abstractions;
using HeroicBrawlServer.Services.Models.Messages;
using HeroicBrawlServer.Shared.Extensions;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HeroicBrawlServer.Services
{
    /// <summary>
    ///     SignalR custom hub
    /// </summary>
    public class RealtimeHub : Hub
    {

        private readonly IRoomService _roomService;
        private readonly IUserService _userService;

        private TimeSpan TickDelay = TimeSpan.FromMilliseconds(33);
        private ICollection<Guid> RoomIds { get; set; }
        private Timer _broadcastLoop;

        #region SignalR entry points

        /// <summary>
        ///     RealtimeHub constructor
        /// </summary>
        /// <param name="roomService"></param>
        public RealtimeHub(IRoomService roomService, IUserService userService)
        {
            _roomService = roomService;
            _userService = userService;

            _broadcastLoop = new Timer(Broadcast, null, TickDelay, TickDelay);
        }

        /// <summary>
        ///     Adds user to a signalR group
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="bearerToken">Access token containing the user id</param>
        /// <returns></returns>
        public async Task JoinRoom(Guid roomId, Guid heroId, string bearerToken)
        {
            var userId = bearerToken.ExtractUserId();
            var user = await _userService.GetByIdAsync(userId);

            await _roomService.AddMemberToRoomAsync(userId, roomId);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
            await SendMessage(roomId, new UserJoinedRoomMessage(userId, user.Pseudo, heroId));
            UpdateRoomList(roomId);
        }

        /// <summary>
        ///     Removes a user from a signalR group
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="bearerToken">Access token containing the user id</param>
        /// <returns></returns>
        public async Task LeaveRoom(Guid roomId, Guid connectionId, string bearerToken)
        {
            var userId = bearerToken.ExtractUserId();

            await _roomService.RemoveMemberFromRoomAsync(userId, roomId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
            await SendMessage(roomId, new UserLeftRoomMessage(userId, connectionId));
        }

        // TODO: entry point to update game state

        #endregion

        #region private functions

        /// <summary>
        ///     For each room, broadcast message for game state
        /// </summary>
        /// <returns></returns>
        public async void Broadcast(object state)
        {
            foreach (var roomId in RoomIds)
            {
                // TODO: Store state in each room and generate message
                var stateMessage = new GameStateMessage();

                await SendMessage(roomId, stateMessage);
            }
        }

        /// <summary>
        ///     Sends a message to all users in the room
        /// </summary>
        /// <param name="roomId">id of the room</param>
        /// <param name="message">message to send</param>
        /// <returns></returns>
        private async Task SendMessage(Guid roomId, string message)
        {
            await Clients.Group(roomId.ToString()).SendAsync(message);
        }
        private async Task SendMessage(Guid roomId, BaseMessage message)
        {
            await SendMessage(roomId, message.ToString());
        }

        private void UpdateRoomList(Guid roomId)
        {
            if (RoomIds.Contains(roomId))
            {
                return;
            }

            RoomIds.Add(roomId);
        }

        #endregion

    }
}
