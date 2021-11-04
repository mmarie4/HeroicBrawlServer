using System;
using System.Threading.Tasks;
using HeroicBrawlServer.Services.Abstractions;
using HeroicBrawlServer.Services.Models.Messages;
using HeroicBrawlServer.Shared.Extensions;
using Microsoft.AspNetCore.SignalR;

namespace HeroicBrawlServer.Services
{
    /// <summary>
    ///     SignalR custom hub
    /// </summary>
    public class RealtimeHub : Hub
    {

        private readonly IRoomService _roomService;
        private readonly IUserService _userService;

        /// <summary>
        ///     RealtimeHub constructor
        /// </summary>
        /// <param name="roomService"></param>
        public RealtimeHub(IRoomService roomService, IUserService userService)
        {
            _roomService = roomService;
            _userService = userService;
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

        /// <summary>
        ///     Sends a message to all users in the room
        /// </summary>
        /// <param name="roomId">id of the room</param>
        /// <param name="message">message to send</param>
        /// <returns></returns>
        public async Task SendMessage(Guid roomId, string message)
        {
            // TODO: Find userId using connectionId. Or just use connectionId in the clients to move others...

            // Do game logic things ... Check cheats ... etc

            await Clients.Group(roomId.ToString()).SendAsync(message);
        }

    }
}
