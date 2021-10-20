using System;
using System.Threading.Tasks;
using HeroicBrawlServer.Services.Abstractions;
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

        public async Task JoinRoom(Guid roomId, string bearerToken)
        {
            // TODO: Join room with token not userId
            var userId = Guid.NewGuid();

            await _roomService.AddMemberToRoomAsync(userId, roomId);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        }

        public async Task LeaveRoom(Guid roomId, string bearerToken)
        {
            // TODO: Join room with token not userId
            var userId = Guid.NewGuid();

            await _roomService.RemoveMemberFromRoomAsync(userId, roomId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
        }

        public async Task SendMessage(Guid roomId, string message)
        {
            // TODO: Find userId using connectionId

            // Do game logic things ... 

            await Clients.Group(roomId.ToString()).SendAsync(message);
        }

    }
}
