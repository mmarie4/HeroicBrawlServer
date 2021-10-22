using System;
using System.Threading.Tasks;
using HeroicBrawlServer.Services.Abstractions;
using HeroicBrawlServer.Shared.Extensions;
using Microsoft.AspNetCore.SignalR;

namespace HeroicBrawlServer.Services
{
    public class RealtimeHub : Hub
    {

        private readonly IRoomService _roomService;

        public RealtimeHub(IRoomService roomService)
        {
            _roomService = roomService;
        }

        /// <summary>
        ///     Adds user to a signalR group
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="bearerToken">Access token containing the user id</param>
        /// <returns></returns>
        public async Task JoinRoom(Guid roomId, string bearerToken)
        {
            var userId = bearerToken.ExtractUserId();

            await _roomService.AddMemberToRoomAsync(userId, roomId);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
            await SendMessage(roomId, $"User {userId} joined"); // TODO: use serialized classes for the messages, not string like this
        }

        /// <summary>
        ///     Removes a user from a signalR group
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="bearerToken">Access token containing the user id</param>
        /// <returns></returns>
        public async Task LeaveRoom(Guid roomId, string bearerToken)
        {
            var userId = bearerToken.ExtractUserId();

            await _roomService.RemoveMemberFromRoomAsync(userId, roomId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
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
